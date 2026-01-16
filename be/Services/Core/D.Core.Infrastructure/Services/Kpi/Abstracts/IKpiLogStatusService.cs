using D.Core.Domain.Dtos.Kpi.KpiLogStatus;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiLogStatusService
    {
        void InsertLog(InsertKpiLogStatusDto dto);
        PageResultDto<KpiLogStatusDto> FindKpiLogs(FindKpiLogStatusDto dto);
    }
}
