using AutoMapper;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Domain.Entities.Survey;
using D.Core.Domain.Entities.Survey.Constants;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
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

            var entity = await _unitOfWork.iKsSurveyRequestRepository.GetDetailWithNavigationsForUpdateAsync(dto.Id);
            if (entity == null) throw new Exception("Không tìm thấy bản ghi.");
            if (entity.TrangThai != RequestStatus.Draft && entity.TrangThai != RequestStatus.Rejected)
                throw new Exception("Chỉ được chỉnh sửa khi phiếu ở trạng thái Nháp hoặc bị Từ chối.");

            string changeDesc = $"Cập nhật thông tin phiếu {entity.MaYeuCau}";

            // Map only scalar properties, not collections (to avoid tracking conflicts)
            entity.MaYeuCau = dto.MaYeuCau;
            entity.TenKhaoSatYeuCau = dto.TenKhaoSatYeuCau;
            entity.MoTa = dto.MoTa;
            entity.ThoiGianBatDau = dto.ThoiGianBatDau;
            entity.ThoiGianKetThuc = dto.ThoiGianKetThuc;
            entity.IdPhongBan = dto.IdPhongBan;

            var oldTargets = entity.Targets.ToList();
            foreach (var item in oldTargets)
            {
                _unitOfWork.iKsSurveyTargetRepository.Delete(item);
            }
            entity.Targets.Clear();

            if (dto.Targets != null)
            {
                foreach (var t in dto.Targets)
                {
                    var newTarget = _mapper.Map<KsSurveyTarget>(t);
                    entity.Targets.Add(newTarget);
                }
            }

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

            var oldQuestions = entity.Questions.ToList();
            foreach (var item in oldQuestions)
            {
                _unitOfWork.iKsSurveyQuestionRepository.Delete(item);
            }
            entity.Questions.Clear();

            if (dto.Questions != null)
            {
                foreach (var q in dto.Questions)
                {
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

            var log = new KsSurveyLog
            {
                IdNguoiThaoTac = userId,
                TenNguoiThaoTac = userName,
                LoaiHanhDong = actionType,
                MoTa = description,
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

        public List<RequestSurveyQuestionDto> ReadExcel(Stream fileStream)
        {
            var questions = new List<RequestSurveyQuestionDto>();

            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(fileStream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                    });
                    if (result.Tables.Count == 0 || !result.Tables.Contains("data"))
                    {
                        throw new Exception("File Excel không đúng định dạng. Thiếu sheet 'data'");
                    }
                    var table = result.Tables["data"];
                    RequestSurveyQuestionDto currentQuestion = null;
                    foreach (DataRow row in table.Rows)
                    {
                        // Skip empty rows
                        if (row.ItemArray.All(field => field == null || string.IsNullOrWhiteSpace(field.ToString())))
                            continue;
                        string noiDungCauHoi = row["Nội dung câu hỏi(*)"]?.ToString()?.Trim();
                        // New question
                        if (!string.IsNullOrEmpty(noiDungCauHoi))
                        {
                            currentQuestion = new RequestSurveyQuestionDto
                            {
                                MaCauHoi = row["Mã câu hỏi(*)"]?.ToString()?.Trim(),
                                NoiDung = noiDungCauHoi,
                                LoaiCauHoi = int.TryParse(row["Loại câu hỏi(*)(1:Đơn, 2:Nhiều, 3:Tự luận)"]?.ToString(), out int loai) ? loai : 1,
                                BatBuoc = row["Bắt buộc (X)"]?.ToString()?.Trim().ToUpper() == "X",
                                Answers = new List<RequestQuestionAnswerDto>()
                            };
                            questions.Add(currentQuestion);
                        }
                        // Answer
                        string noiDungDapAn = row["Đáp án(*)"]?.ToString()?.Trim();
                        if (currentQuestion != null && !string.IsNullOrEmpty(noiDungDapAn))
                        {
                            currentQuestion.Answers.Add(new RequestQuestionAnswerDto
                            {
                                NoiDung = noiDungDapAn,
                                Value = int.TryParse(row["Điểm (Value)"]?.ToString(), out int val) ? val : 0,
                                IsCorrect = row["Đáp án đúng? (X)"]?.ToString()?.Trim().ToUpper() == "X"
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi đọc file Excel: {ex.Message}");
            }
            return questions;
        }

    }
}
