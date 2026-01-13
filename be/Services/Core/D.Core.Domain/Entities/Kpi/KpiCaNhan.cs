using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Kpi
{
    [Table(nameof(KpiCaNhan), Schema = DbSchema.Kpi)]
    public class KpiCaNhan : EntityBase
    {
        public int? STT { get; set; }

        [MaxLength(500)]
        public string? KPI { get; set; }
        public string? LinhVuc { get; set; }
        public string? ChienLuoc { get; set; }

        [MaxLength(255)]
        public string? MucTieu { get; set; }

        [MaxLength(255)]
        public string? TrongSo { get; set; }

        public int? LoaiKPI { get; set; }

        public int IdNhanSu { get; set; }

        public int? IdKpiDonVi { get; set; }

        public string? Role { get; set; }

        public string? NamHoc { get; set; }

        public int? Status { get; set; }
        public float? TyLeThamGia { get; set; }

        public decimal? KetQuaThucTe { get; set; }
        public string? CongThucTinh { get; set; }
        public string? LoaiKetQua { get; set; }
        public int? IdCongThuc { get; set; }
        public decimal? DiemKpi { get; set; }
        public bool? IsCaNhanKeKhai { get; set; }
        public decimal? CapTrenDanhGia { get; set; }
        public decimal? DiemKpiCapTren { get; set; }
        [MaxLength(1000)]
        public string? GhiChu { get; set; }
    }
}
