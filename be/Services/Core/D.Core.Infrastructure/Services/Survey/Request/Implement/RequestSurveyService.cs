using AutoMapper;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Domain.Entities.Survey;
using D.Core.Domain.Entities.Survey.Constants;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace D.Core.Infrastructure.Services.Survey.Request.Implement
{
    public class RequestSurveyService : ServiceBase, IRequestSurveyService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IHttpContextAccessor _httpContextAccessor;

        public RequestSurveyService(
            ILogger<RequestSurveyService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContext;
        }

        public PageResultDto<RequestSurveyResponseDto> Paging(FilterSurveyRequestDto dto)
        {
            var query = from r in _unitOfWork.iKsSurveyRequestRepository.TableNoTracking
                        join pb in _unitOfWork.iDmPhongBanRepository.TableNoTracking
                            on r.IdPhongBan equals pb.Id into pbJoin
                        from pb in pbJoin.DefaultIfEmpty()

                        where (string.IsNullOrEmpty(dto.Keyword) || r.TenKhaoSatYeuCau.Contains(dto.Keyword) || r.MaYeuCau.Contains(dto.Keyword))
                           && (!dto.TrangThai.HasValue || r.TrangThai == dto.TrangThai)

                        orderby r.Id
                        select new RequestSurveyResponseDto
                        {
                            Id = r.Id,
                            MaYeuCau = r.MaYeuCau,
                            TenKhaoSatYeuCau = r.TenKhaoSatYeuCau,
                            MoTa = r.MoTa,
                            ThoiGianBatDau = r.ThoiGianBatDau,
                            ThoiGianKetThuc = r.ThoiGianKetThuc,
                            IdPhongBan = r.IdPhongBan,
                            TrangThai = r.TrangThai,
                            LyDoTuChoi = r.LyDoTuChoi
                        };

            var totalCount = query.Count();
            var items = query.Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            return new PageResultDto<RequestSurveyResponseDto>
            {
                Items = items,
                TotalItem = totalCount,

            };
        }

        public async Task<RequestSurveyDetailDto> GetByIdRequest(int id)
        {
            var entity = await _unitOfWork.iKsSurveyRequestRepository.GetDetailWithNavigationsAsync(id);
            if (entity == null) throw new Exception("Không tìm thấy yêu cầu khảo sát.");

            return _mapper.Map<RequestSurveyDetailDto>(entity);
        }

        public async Task<CreateRequestSurveyResponseDto> CreateRequestSurvey(CreateRequestSurveyRequestDto dto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            // Validate
            if (_unitOfWork.iKsSurveyRequestRepository.IsMaYeuCauExist(dto.MaYeuCau))
                throw new Exception($"Mã yêu cầu {dto.MaYeuCau} đã tồn tại.");

            if (dto.ThoiGianKetThuc < dto.ThoiGianBatDau)
                throw new Exception("Thời gian kết thúc phải sau thời gian bắt đầu.");

            // Map & Save
            var entity = _mapper.Map<KsSurveyRequest>(dto);
            entity.TrangThai = RequestStatus.Draft;
         
            await _unitOfWork.iKsSurveyRequestRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // Log
            await LogActionAsync(entity.MaYeuCau, "Create", "Thêm mới yêu cầu khảo sát", null, "Draft");
            scope.Complete();
            return _mapper.Map<CreateRequestSurveyResponseDto>(entity);
        }

        public async Task<UpdateRequestSurveyResponseDto> UpdateRequestSurvey(UpdateRequestSurveyRequestDto dto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var entity = await _unitOfWork.iKsSurveyRequestRepository.GetDetailWithNavigationsAsync(dto.Id);
            if (entity == null) throw new Exception("Không tìm thấy bản ghi.");
            if (entity.TrangThai != RequestStatus.Draft && entity.TrangThai != RequestStatus.Rejected)
                throw new Exception("Chỉ được chỉnh sửa khi phiếu ở trạng thái Nháp hoặc bị Từ chối.");

            string changeDesc = $"Cập nhật thông tin phiếu {entity.MaYeuCau}";

            _mapper.Map(dto, entity);

            var oldTargets = entity.Targets.ToList(); // .ToList() để copy ra mảng tạm, tránh lỗi
            foreach (var item in oldTargets)
            {
                // Gọi Repo xóa item này đi
                // Lưu ý: Ông cần đảm bảo UnitOfWork có repo này hoặc dùng Context delete trực tiếp
                _unitOfWork.iKsSurveyTargetRepository.Delete(item);
            }
            entity.Targets.Clear(); // Xóa trong list memory

            // Bước B: Thêm cái mới
            if (dto.Targets != null)
            {
                foreach (var t in dto.Targets)
                {
                    var newTarget = _mapper.Map<KsSurveyTarget>(t);
                    // newTarget.Id = 0; // Đảm bảo ID = 0 để nó hiểu là thêm mới
                    entity.Targets.Add(newTarget);
                }
            }

            // =======================================================================
            // 3. XỬ LÝ CRITERIA (Tiêu chí)
            // =======================================================================
            var oldCriterias = entity.Criterias.ToList();
            foreach (var item in oldCriterias)
            {
                _unitOfWork.iKsSurveyCriteriaRepository.Delete(item);
            }
            entity.Criterias.Clear();

            if (dto.Criterias != null)
            {
                foreach (var c in dto.Criterias)
                {
                    entity.Criterias.Add(_mapper.Map<KsSurveyCriteria>(c));
                }
            }

            // =======================================================================
            // 4. XỬ LÝ QUESTION (Câu hỏi) & ANSWER (Đáp án)
            // =======================================================================
            var oldQuestions = entity.Questions.ToList();
            foreach (var item in oldQuestions)
            {
                // Khi xóa Question, nhớ đảm bảo trong SQL đã set "ON DELETE CASCADE" cho bảng Answers
                // Nếu không là phải loop xóa Answer trước đấy.
                _unitOfWork.iKsSurveyQuestionRepository.Delete(item);
            }
            entity.Questions.Clear();

            if (dto.Questions != null)
            {
                foreach (var q in dto.Questions)
                {
                    // Map câu hỏi + câu trả lời con bên trong
                    entity.Questions.Add(_mapper.Map<KsSurveyQuestion>(q));
                }
            }

            _unitOfWork.iKsSurveyRequestRepository.Update(entity);

            // Log
            await LogActionAsync(entity.MaYeuCau, "Update", changeDesc, null, null);
            await _unitOfWork.SaveChangesAsync();
            scope.Complete();

            return _mapper.Map<UpdateRequestSurveyResponseDto>(entity);
        }

        public async Task<bool> DeleteRequestSurvey(DeleteRequestSurveyDto dto)
        {
            var entity =  _unitOfWork.iKsSurveyRequestRepository.FindById(dto.Id);
            if (entity == null) throw new Exception("Không tìm thấy bản ghi.");
            if (entity.TrangThai != RequestStatus.Draft && entity.TrangThai != RequestStatus.Rejected)
                throw new Exception("Chỉ được xóa phiếu Nháp hoặc phiếu bị Từ chối.");

            var oldStatus = entity.TrangThai;
            entity.TrangThai = RequestStatus.Canceled;

            _unitOfWork.iKsSurveyRequestRepository.Update(entity);

            await LogActionAsync(
                entity.MaYeuCau,
                "Cancel",
                "Hủy/Đóng yêu cầu khảo sát",
                oldStatus.ToString(),
                RequestStatus.Canceled.ToString()
            );

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task SubmitRequestAsync(int id)
        {
            var entity = _unitOfWork.iKsSurveyRequestRepository.FindById(id);
            if (entity == null) throw new Exception("Không tìm thấy bản ghi.");

            if (entity.TrangThai != RequestStatus.Draft && entity.TrangThai != RequestStatus.Rejected)          
                throw new Exception("Chỉ được gửi duyệt khi phiếu ở trạng thái Nháp hoặc đã bị Từ chối.");      

            var oldStatus = entity.TrangThai;

            entity.TrangThai = RequestStatus.Pending;

            // resubmit > clear reject reason
            entity.LyDoTuChoi = null;

            _unitOfWork.iKsSurveyRequestRepository.Update(entity);

            // Log
            await LogActionAsync(
                entity.MaYeuCau,
                "Submit",
                "Gửi duyệt yêu cầu (Chuyển sang trạng thái Chờ duyệt)",
                oldStatus.ToString(),
                RequestStatus.Pending.ToString()
            );

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelSubmitAsync(int id)
        {
            var entity = _unitOfWork.iKsSurveyRequestRepository.FindById(id);
            if (entity == null) throw new Exception("Không tìm thấy bản ghi.");

            if (entity.TrangThai != RequestStatus.Pending)
                throw new Exception("Chỉ có thể hủy gửi duyệt khi phiếu đang ở trạng thái Chờ duyệt.");

            var oldStatus = entity.TrangThai;

            entity.TrangThai = RequestStatus.Draft;

            _unitOfWork.iKsSurveyRequestRepository.Update(entity);

            // Log
            await LogActionAsync(
                entity.MaYeuCau,
                "CancelSubmit",
                "Hủy gửi duyệt - Rút yêu cầu về lại Nháp",
                oldStatus.ToString(),
                RequestStatus.Draft.ToString()
            );

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task LogActionAsync(string targetId, string actionType, string description, string? oldValue = null, string? newValue = null)
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContextAccessor);

            var user = await _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .FirstOrDefaultAsync(u => u.Id == userId);

            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown";

            var fullDescription = $"{description}. Thực hiện bởi {userName} vào {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            var log = new KsSurveyLog
            {
                IdNguoiThaoTac = userId,
                TenNguoiThaoTac = userName,
                LoaiHanhDong = actionType,
                MoTa = fullDescription,
                TenBang = nameof(KsSurveyRequest),
                IdDoiTuong = targetId,
                DuLieuCu = oldValue,
                DuLieuMoi = newValue,
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.iKsSurveyLogRepository.AddAsync(log);
        }

        public async Task ApproveRequestAsync(int id)
        {
            var entity = _unitOfWork.iKsSurveyRequestRepository.FindById(id);
            if (entity == null) throw new Exception("Không tìm thấy bản ghi.");

            if (entity.TrangThai != RequestStatus.Pending)
                throw new Exception("Chỉ được duyệt phiếu đang ở trạng thái Chờ duyệt.");

            var oldStatus = entity.TrangThai;
            entity.TrangThai = RequestStatus.Approved;
            entity.LyDoTuChoi = null;

            _unitOfWork.iKsSurveyRequestRepository.Update(entity);

            // Log
            await LogActionAsync(
                entity.MaYeuCau,
                "Approve",
                "Duyệt yêu cầu khảo sát",
                oldStatus.ToString(),
                RequestStatus.Approved.ToString()
            );

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RejectRequestAsync(RejectRequestDto dto)
        {
            var entity = _unitOfWork.iKsSurveyRequestRepository.FindById(dto.Id);
            if (entity == null) throw new Exception("Không tìm thấy bản ghi.");

            if (entity.TrangThai != RequestStatus.Pending)
                throw new Exception("Chỉ được từ chối phiếu đang ở trạng thái Chờ duyệt.");

            if (string.IsNullOrWhiteSpace(dto.Reason))
                throw new Exception("Vui lòng nhập lý do từ chối.");

            var oldStatus = entity.TrangThai;
            entity.TrangThai = RequestStatus.Rejected;
            entity.LyDoTuChoi = dto.Reason;

            _unitOfWork.iKsSurveyRequestRepository.Update(entity);

            // Log
            await LogActionAsync(
                entity.MaYeuCau,
                "Reject",
                $"Từ chối yêu cầu. Lý do: {dto.Reason}",
                oldStatus.ToString(),
                RequestStatus.Rejected.ToString()
            );

            await _unitOfWork.SaveChangesAsync();
        }

    }
}
