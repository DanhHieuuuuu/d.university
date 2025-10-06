namespace D.Core.Domain.Dtos.Hrm
{
    public class NsNhanSuResponseDto
    {
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }
        public string? HoDem { get; set; }
        public string? Ten { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public int? GioiTinh { get; set; }
        public string? SoCccd { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? TenChucVu { get; set; }
        public string? TenPhongBan { get; set; }
    }
}
