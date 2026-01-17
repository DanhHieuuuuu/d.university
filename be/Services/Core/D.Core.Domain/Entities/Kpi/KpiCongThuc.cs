using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Kpi
{
    [Table(nameof(KpiCongThuc), Schema = DbSchema.Kpi)]
    public class KpiCongThuc : EntityBase
    {
        public string? TenCongThuc { get; set; }
        public int isActive { get; set; } = 1;
        }
}
