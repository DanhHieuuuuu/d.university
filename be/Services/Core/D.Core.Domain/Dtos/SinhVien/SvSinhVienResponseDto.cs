namespace D.Core.Domain.Dtos.SinhVien
{
    public class SvSinhVienResponseDto
    {
        public int IdSinhVien { get; set; }
        public string? Mssv { get; set; }
        public string? HoDem { get; set; }
        public string? Ten { get; set; }
        public string HoTen => $"{HoDem} {Ten}".Trim();
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public int? GioiTinh { get; set; }
        public int? QuocTich { get; set; }
        public int? DanToc { get; set; }
        public int? TonGiao { get; set; }
        public string? NguyenQuan { get; set; }
        public string? NoiOHienTai { get; set; }
        public string? SoCccd { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        public string? Email2 { get; set; }
        public int? KhoaHoc { get; set; }
        public int? Khoa { get; set; }
        public int? Nganh { get; set; }
        public int? LopQL { get; set; }
        public int? TrangThaiHoc { get; set; }
    }
}
