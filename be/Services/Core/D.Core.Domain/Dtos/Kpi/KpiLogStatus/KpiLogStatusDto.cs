namespace D.Core.Domain.Dtos.Kpi.KpiLogStatus
{
    public class KpiLogStatusDto
    {
        public int Id { get; set; }
        public int? OldStatus { get; set; }
        public int? NewStatus { get; set; }
        public string? Description { get; set; }
        public string? Reason { get; set; }
        public int? CapKpi { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
