namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc
{
    public class DmKhoaHocResponseDto
    {
        public int Id { get; set; }
        public string? MaKhoaHoc { get; set; }
        public string? TenKhoaHoc { get; set; }
        public string? Nam { get; set; }
        public string? CachViet { get; set; }
        public int? NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
