using D.DomainBase.Common;


namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class KpiCaNhanDto
    {
        public int Id { get; set; }
        public string? KPI { get; set; }
        public int STT { get; set; }
        public string? LinhVuc { get; set; }
        public string? MucTieu { get; set; }
        public string? TrongSo { get; set; }
        public int? LoaiKpi { get; set; }
        public string? LoaiKPIText { get; set; }
        public string? NhanSu { get; set; }
        public int? IdNhanSu { get; set; }
        public string? PhongBan { get; set; }
        public string? NamHoc { get; set; }
        public int? TrangThai { get; set; }
        public string? TrangThaiText { get; set; }
        public decimal? KetQuaThucTe { get; set; }
        public float? TyLeThamGia { get; set; }
    }
}
