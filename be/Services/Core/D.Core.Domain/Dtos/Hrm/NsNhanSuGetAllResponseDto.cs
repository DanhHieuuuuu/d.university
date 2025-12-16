namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class NsNhanSuGetAllResponseDto
    {
        public int Id { get; set; }
        public string? MaNhanSu { get; set; }
        public string? HoDem { get; set; }

        public string? Ten { get; set; }
        public string? SoCccd { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? TenChucVu { get; set; }
        public string? TenPhongBan { get; set; }
        public string? TrangThai { get; set; } 
    }

}


