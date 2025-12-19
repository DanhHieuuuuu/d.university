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
    public class RequestService : ServiceBase, IRequestService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IHttpContextAccessor _httpContextAccessor;

        public RequestService(
            ILogger<RequestService> logger,
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

                        orderby r.Id descending
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

            // 1. Validate
            if (_unitOfWork.iKsSurveyRequestRepository.IsMaYeuCauExist(dto.MaYeuCau))
                throw new Exception($"Mã yêu cầu {dto.MaYeuCau} đã tồn tại.");

            if (dto.ThoiGianKetThuc < dto.ThoiGianBatDau)
                throw new Exception("Thời gian kết thúc phải sau thời gian bắt đầu.");

            // 2. Map & Save
            var entity = _mapper.Map<KsSurveyRequest>(dto);
            entity.TrangThai = RequestStatus.Draft;
         
            await _unitOfWork.iKsSurveyRequestRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // 3. Log
            await LogActionAsync(entity.MaYeuCau, "Create", "Thêm mới yêu cầu khảo sát", null, "Draft");
            scope.Complete();
            return _mapper.Map<CreateRequestSurveyResponseDto>(entity);
        }

        public async Task<UpdateRequestSurveyResponseDto> UpdateRequestSurvey(UpdateRequestSurveyRequestDto dto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var entity = await _unitOfWork.iKsSurveyRequestRepository.GetDetailWithNavigationsAsync(dto.Id);
            if (entity == null) throw new Exception("Không tìm thấy bản ghi.");
            if (entity.TrangThai != RequestStatus.Draft) throw new Exception("Chỉ được chỉnh sửa khi phiếu ở trạng thái Nháp.");

            // Detect change 
            string changeDesc = $"Cập nhật thông tin phiếu {entity.MaYeuCau}";

            // Map Main Info
            _mapper.Map(dto, entity);

            // Map Children (Clear -> Add)
            entity.Targets.Clear();
            if (dto.Targets != null)
                foreach (var t in dto.Targets) entity.Targets.Add(_mapper.Map<KsSurveyTarget>(t));

            entity.Criterias.Clear();
            if (dto.Criterias != null)
                foreach (var c in dto.Criterias) entity.Criterias.Add(_mapper.Map<KsSurveyCriteria>(c));

            entity.Questions.Clear();
            if (dto.Questions != null)
                foreach (var q in dto.Questions) entity.Questions.Add(_mapper.Map<KsSurveyQuestion>(q));

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
            if (entity.TrangThai != RequestStatus.Draft) throw new Exception("Chỉ được hủy khi phiếu ở trạng thái Nháp.");

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
            await ChangeStatusAsync(id, RequestStatus.Draft, RequestStatus.Pending, "Gửi duyệt yêu cầu");
        }

        public async Task CancelSubmitAsync(int id)
        {
            await ChangeStatusAsync(id, RequestStatus.Pending, RequestStatus.Draft, "Hủy gửi duyệt - Về lại Nháp");
        }

        private async Task ChangeStatusAsync(int id, int oldStatusExpect, int newStatus, string actionDesc)
        {
            var entity = _unitOfWork.iKsSurveyRequestRepository.FindById(id);
            if (entity == null) throw new Exception("Không tìm thấy bản ghi.");

            if (entity.TrangThai != oldStatusExpect)
                throw new Exception($"Trạng thái không hợp lệ. (Hiện tại: {entity.TrangThai})");

            var oldStatus = entity.TrangThai;
            entity.TrangThai = newStatus;

            _unitOfWork.iKsSurveyRequestRepository.Update(entity);

            // Log
            await LogActionAsync(entity.MaYeuCau, "ChangeStatus", actionDesc, oldStatus.ToString(), newStatus.ToString());

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

    }
}
