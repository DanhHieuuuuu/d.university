using AutoMapper;
using D.Core.Domain.Dtos.Kpi.KpiLogStatus;
using D.Core.Domain.Entities.Kpi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiLogStatusService : ServiceBase, IKpiLogStatusService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public KpiLogStatusService(
            ILogger<KpiLogStatusService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public PageResultDto<KpiLogStatusDto> FindKpiLogs(FindKpiLogStatusDto dto)
        {
            var query = _unitOfWork.iKpiLogStatusRepository.TableNoTracking
                .AsQueryable();

            if (dto.KpiId.HasValue)
                query = query.Where(x => x.KpiId == dto.KpiId);

            if (dto.CapKpi.HasValue)
                query = query.Where(x => x.CapKpi == dto.CapKpi);

            var totalCount = query.Count();

            var items = query
                .OrderByDescending(x => x.CreatedDate)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            return new PageResultDto<KpiLogStatusDto>
            {
                Items = _mapper.Map<List<KpiLogStatusDto>>(items),
                TotalItem = totalCount
            };
        }

        public void InsertLog(InsertKpiLogStatusDto dto)
        {
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .FirstOrDefault(u => u.Id == userId);

            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown";

            var log = new KpiLogStatus
            {
                KpiId = dto.KpiId,
                OldStatus = dto.OldStatus,
                NewStatus = dto.NewStatus,
                Description = dto.Description,
                Reason = dto.Reason,
                CapKpi = dto.CapKpi,
                CreatedBy = userId.ToString(),
                CreatedByName = userName,
                CreatedDate = DateTime.Now
            };

            _unitOfWork.iKpiLogStatusRepository.Add(log);
            _unitOfWork.iKpiLogStatusRepository.SaveChange();
        }
    }
}
