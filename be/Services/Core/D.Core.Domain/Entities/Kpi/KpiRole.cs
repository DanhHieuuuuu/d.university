using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Kpi
{
    [Table(nameof(KpiRole), Schema = DbSchema.Kpi)]
    public class KpiRole : EntityBase
    {
        public string? Role { get; set; }
        public int IdNhanSu { get; set; }
        public int? IdDonVi { get; set; }
        [Precision(5, 2)]
        public decimal? TiLe { get; set; }
    }
}
