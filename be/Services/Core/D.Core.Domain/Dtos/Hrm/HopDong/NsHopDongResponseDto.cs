namespace D.Core.Domain.Dtos.Hrm.HopDong
{
    public class NsHopDongResponseDto
    {
        public int Id { get; set; }
        public string? SoHopDong { get; set; }
        public int? IdNhanSu { get; set; }
        public string? HoTen { get; set; }
        public int? IdLoaiHopDong { get; set; }
        public DateTime? NgayKyKet { get; set; }
        public DateTime? NgayBatDauThuViec { get; set; }
        public DateTime? NgayKetThucThuViec { get; set; }
        public DateTime? HopDongCoThoiHanTuNgay { get; set; }
        public DateTime? HopDongCoThoiHanDenNgay { get; set; }
        public int? LuongCoBan { get; set; }
        public string? GhiChu { get; set; }
    }
}
