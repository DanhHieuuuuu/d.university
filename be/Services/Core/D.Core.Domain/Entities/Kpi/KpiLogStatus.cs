using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Kpi
{
    [Table(nameof(KpiLogStatus), Schema = DbSchema.Kpi)]
    public class KpiLogStatus : EntityBase
    {
        [Description("Mã KPI")]
        public int? KpiId { get; set; }

        [Description("Trạng thái cũ")]
        public int? OldStatus { get; set; }

        [Description("Trạng thái mới")]
        public int? NewStatus { get; set; }

        [Description("Mô tả")]
        [MaxLength(255)]
        public string? Description { get; set; }

        [Description("Lý do")]
        [MaxLength(255)]
        public string? Reason { get; set; }
    }
}
