using AutoMapper;
using D.Core.Domain.Dtos.Survey.Surveys;
using D.Core.Domain.Entities.Survey;
using D.Core.Domain.Entities.Survey.Constants;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Minio.DataModel.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace D.Core.Infrastructure.Services.Survey.Surveys.Implement
{
    public class SurveyService : ServiceBase, ISurveyService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IHttpContextAccessor _httpContextAccessor;

        public SurveyService(
            ILogger<SurveyService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContext;
        }

        public PageResultDto<SurveyResponseDto> Paging(FilterSurveyDto dto)
        {
            var query = from s in _unitOfWork.iKsSurveyRepository.TableNoTracking

                        where (string.IsNullOrEmpty(dto.Keyword) || s.TenKhaoSat.Contains(dto.Keyword) || s.MaKhaoSat.Contains(dto.Keyword))
                           && (!dto.Status.HasValue || s.Status == dto.Status)

                        orderby s.Id
                        select new SurveyResponseDto
                        {
                            Id = s.Id,
                            MaKhaoSat = s.MaKhaoSat,
                            TenKhaoSat = s.TenKhaoSat,
                            ThoiGianBatDau = s.ThoiGianBatDau,
                            ThoiGianKetThuc = s.ThoiGianKetThuc,
                            Status = s.Status,
                        };

            var totalCount = query.Count();
            var items = query.Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            return new PageResultDto<SurveyResponseDto>
            {
                Items = items,
                TotalItem = totalCount,

            };
        }

        public async Task<SurveyDetailDto> GetByIdSurvey(int id)
        {
            var entity = await _unitOfWork.iKsSurveyRepository.TableNoTracking
                .Include(s => s.SurveyRequest)
                    .ThenInclude(r => r.Questions)
                        .ThenInclude(q => q.Answers)
                .Include(s => s.SurveyRequest)
                    .ThenInclude(r => r.Targets)
                .Include(s => s.SurveyRequest) 
                    .ThenInclude(r => r.Criterias)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) throw new Exception("Không tìm thấy khảo sát.");

            return _mapper.Map<SurveyDetailDto>(entity);
        }

        public async Task CreateSurveyFromRequestAsync(int requestId)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var request = _unitOfWork.iKsSurveyRequestRepository.FindById(requestId);
            if (request == null) throw new Exception("Không tìm thấy yêu cầu gốc.");

            var exists = await _unitOfWork.iKsSurveyRepository.TableNoTracking
                .AnyAsync(x => x.IdYeuCau == requestId);

            if (exists) return;

            var survey = new KsSurvey
            {
                IdYeuCau = request.Id,
                MaKhaoSat = request.MaYeuCau,
                TenKhaoSat = request.TenKhaoSatYeuCau,
                MoTa = request.MoTa,
                ThoiGianBatDau = request.ThoiGianBatDau,
                ThoiGianKetThuc = request.ThoiGianKetThuc,
                IdPhongBan = request.IdPhongBan,
                Status = SurveyStatus.Close,
                CreatedDate = DateTime.Now,
                CreatedBy = CommonUntil.GetCurrentUserId(_httpContextAccessor).ToString()
            };

            await _unitOfWork.iKsSurveyRepository.AddAsync(survey);
            await _unitOfWork.SaveChangesAsync();

            await LogActionAsync(survey.MaKhaoSat, "Create", "Khởi tạo khảo sát", null, "Close");
            scope.Complete();
        }

        public async Task OpenSurveyAsync(int id)
        {
            var entity = _unitOfWork.iKsSurveyRepository.FindById(id);
            if (entity == null) throw new Exception("Không tìm thấy khảo sát.");
            if (entity.Status == SurveyStatus.Open) return;
            
            var oldStatus = entity.Status;
            entity.Status = SurveyStatus.Open;

            _unitOfWork.iKsSurveyRepository.Update(entity);

            await LogActionAsync(
                entity.MaKhaoSat,
                "Open",
                "Mở khảo sát",
                oldStatus.ToString(),
                SurveyStatus.Open.ToString()
            );

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CloseSurveyAsync(int id)
        {
            var entity = _unitOfWork.iKsSurveyRepository.FindById(id);
            if (entity == null) throw new Exception("Không tìm thấy khảo sát.");
            if (entity.Status == SurveyStatus.Close) return;
            
            var oldStatus = entity.Status;
            entity.Status = SurveyStatus.Close;
            
            _unitOfWork.iKsSurveyRepository.Update(entity);

            await LogActionAsync(
                entity.MaKhaoSat,
                "Close",
                "Đóng khảo sát (Ngưng kích hoạt)",
                oldStatus.ToString(),
                SurveyStatus.Close.ToString()
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
                TenBang = nameof(KsSurvey),
                IdDoiTuong = targetId,
                DuLieuCu = oldValue,
                DuLieuMoi = newValue,
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.iKsSurveyLogRepository.AddAsync(log);
        }
    }
}
