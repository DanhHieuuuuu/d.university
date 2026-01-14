using D.Core.Domain.Entities.Kpi;
using D.DomainBase.Common;


namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class KpiCaNhanDto
    {
        public int Id { get; set; }
        public string? KPI { get; set; }
        public string? LinhVuc { get; set; }
        public string? MucTieu { get; set; }
        public string? ChienLuoc { get; set; }
        public string? TrongSo { get; set; }
        public int? LoaiKpi { get; set; }
        public string? NhanSu { get; set; }
        public int? IdNhanSu { get; set; }
        public string? PhongBan { get; set; }
        public string? NamHoc { get; set; }
        public int? TrangThai { get; set; }
        public string? Role { get; set; }
        public int? IdCongThuc { get; set; }
        public string? CongThuc { get; set; }
        public decimal? KetQuaThucTe { get; set; }
        public float? TyLeThamGia { get; set; }
        public decimal? CapTrenDanhGia { get; set; }
        public decimal? DiemKpiCapTren { get; set; }
        public decimal? DiemKpi { get; set; }
        public string? LoaiKetQua { get; set; }
        public int? IsActive { get; set; }
        public bool? IsCaNhanKeKhai { get; set; }
        public string? GhiChu { get; set; }
        public decimal? TongDiemTichHop { get; set; }
    }
}
