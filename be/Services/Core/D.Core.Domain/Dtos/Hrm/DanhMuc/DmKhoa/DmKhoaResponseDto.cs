namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa
{
    public class DmKhoaResponseDto
    {
        public int Id { get; set; }
        public string? MaKhoa { get; set; }
        public string? TenKhoa { get; set; }
        public string? Nam { get; set; }
        public string? CachViet { get; set; }
        public int? NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
