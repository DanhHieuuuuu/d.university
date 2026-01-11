using D.DomainBase.Common;
using D.DomainBase.Dto;

namespace D.Core.Domain.Dtos.Kpi.KpiLogStatus
{
    public class FindKpiLogStatusDto : FilterBaseDto, IQuery<PageResultDto<KpiLogStatusDto>>
    {
        public int? KpiId { get; set; }
        public int? CapKpi { get; set; }
    }
}
