namespace D.Core.Domain.Dtos.SinhVien
{
    public class SvSinhVienGetAllResponseDto
    {
        public int? IdStudent { get; set; }
        public string? Mssv { get; set; }
        public string? HoDem { get; set; }
        public string? Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public string? SoCccd { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        //public string? TenKhoa { get; set; }
        public int? Khoa { get; set; }
        public string? NganhHoc { get; set; }
        public string? TrangThai { get; set; }

    }
}
