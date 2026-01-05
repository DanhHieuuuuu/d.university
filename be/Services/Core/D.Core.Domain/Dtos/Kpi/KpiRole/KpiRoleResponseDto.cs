namespace D.Core.Domain.Dtos.Kpi.KpiRole
{
    public class KpiRoleResponseDto
    {
        public int ID { get; set; }
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }
        public string? TenNhanSu { get; set; }
        public string? TenPhongBan { get; set; }
        public int? IdDonVi { get; set; }
        public string? TenDonViKiemNhiem { get; set; }
        public decimal? TiLe { get; set; }
        public string? Role { get; set; }
    }
}
