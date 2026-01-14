using D.DomainBase.Common;


namespace D.Core.Domain.Dtos.Kpi.KpiLogStatus
{
    public class InsertKpiLogStatusDto : ICommand
    {
        public int? KpiId { get; set; }
        public int? OldStatus { get; set; }
        public int? NewStatus { get; set; }
        public string? Description { get; set; }
        public string? Reason { get; set; }
        public int? CapKpi { get; set; }
    }
}
