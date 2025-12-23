using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Kpi
{
    [Table(nameof(KpiDonVi), Schema = DbSchema.Kpi)]
    public class KpiDonVi : EntityBase
    {
        [MaxLength(500)]
        public string? Kpi { get; set; }
        [MaxLength(250)]
        public string? MucTieu { get; set; }
        [MaxLength(250)]
        public string? TrongSo { get; set; }
        public int? IdDonVi { get; set; }
        public int? LoaiKpi { get; set; }
        [MaxLength(4)]
        public string? NamHoc { get; set; }
        public int? TrangThai { get; set; }
        public int? IdKpiTruong { get; set; }
        public decimal? KetQuaThucTe { get; set; }
        public string? CongThucTinh { get; set; }
        public LoaiCongThuc? LoaiCongThuc { get; set; }
        public string? ThamSoCongThuc { get; set; }
        public decimal? DiemKpi { get; set; }
    }
}
