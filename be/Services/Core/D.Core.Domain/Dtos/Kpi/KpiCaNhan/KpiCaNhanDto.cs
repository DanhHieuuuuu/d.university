using D.DomainBase.Common;


namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class KpiCaNhanDto
    {
        public int Id { get; set; }
        public string? KPI { get; set; }
        public int STT { get; set; }
        public string? MucTieu { get; set; }
        public string? TrongSo { get; set; }
        public int LoaiKPI { get; set; }
        public string? NhanSu { get; set; }
        public string? PhongBan { get; set; }
        public string? NamHoc { get; set; }
        public int? TrangThai { get; set; }
        public decimal? KetQuaThucTe { get; set; }
    }
}
