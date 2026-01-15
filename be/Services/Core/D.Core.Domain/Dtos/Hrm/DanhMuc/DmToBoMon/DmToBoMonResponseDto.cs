namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon
{
    public class DmToBoMonResponseDto
    {
        public int Id { get; set; }
        public string? MaBoMon { get; set; }
        public string? TenBoMon { get; set; }
        public DateTime? NgayThanhLap { get; set; }
        public int? IdPhongBan { get; set; }
        public string? PhongBan { get; set; }
    }
}
