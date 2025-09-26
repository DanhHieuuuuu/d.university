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
        public bool? GioiTinh { get; set; }
        public string? SoCccd { get; set; }
    }
}
