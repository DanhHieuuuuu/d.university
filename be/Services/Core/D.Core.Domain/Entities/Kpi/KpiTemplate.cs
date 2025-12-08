using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace D.Core.Domain.Entities.Kpi
{
    [Table(nameof(KpiTemplate), Schema = DbSchema.Kpi)]
    public class KpiTemplate : EntityBase
    {
        [MaxLength(500)]
        public string? KPI { get; set; }
        public int STT { get; set; }
        [MaxLength(250)]
        public string? MucTieu { get; set; }
        [MaxLength(250)]
        public string? TrongSo { get; set; }
        public int LoaiKPI { get; set; }
        public int LoaiTemplate { get; set; }
        public string? NamHoc { get; set; }
    }
}
