namespace D.Core.Domain.Dtos.Hrm.DanhMuc
{
    public class CreateDmToBoMonDto
    {
        public string? MaBoMon { get; set; }
        public string? TenBoMon { get; set; }
        public DateTime? NgayThanhLap { get; set; } = DateTime.Now;
        public int? IdPhongBan { get; set; }
    }
}
