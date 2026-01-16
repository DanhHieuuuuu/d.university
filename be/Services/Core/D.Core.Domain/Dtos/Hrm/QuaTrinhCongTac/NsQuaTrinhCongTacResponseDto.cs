namespace D.Core.Domain.Dtos.Hrm.QuaTrinhCongTac
{
    public class NsQuaTrinhCongTacResponseDto
    {
        public int Id { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? IdChucVu { get; set; }
        public int? IdPhongBan { get; set; }
        public int? IdToBoMon { get; set; }
        public string? Description { get; set; }
    }
}
