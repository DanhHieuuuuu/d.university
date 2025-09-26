using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class CreateHopDongDto : ICommand
    {
        public string? SoHopDong { get; set; }
        public int? IdLoaiHopDong { get; set; }
        public DateTime? NgayKyKet { get; set; }
        public DateTime? KyLan1 { get; set; }
        public DateTime? KyLan2 { get; set; }
        public DateTime? KyLan3 { get; set; }
        public DateTime? NgayBatDauThuViec { get; set; }
        public DateTime? NgayKetThucThuViec { get; set; }
        public DateTime? HopDongCoThoiHanTuNgay { get; set; }
        public DateTime? HopDongCoThoiHanDenNgay { get; set; }

        // Chi tiết hợp đồng
        public int? LuongCoBan { get; set; }
        public int? IdToBoMon { get; set; }
        public int? IdPhongBan { get; set; }
        public int IdChucVu { get; set; }
        public string? GhiChu { get; set; }

        // Nếu nhân sự đã tồn tại trong db thì chỉ cần truyền IdNhanSu
        // Nếu nhân sự chưa tồn tại trong db thì truyền ThongTinNhanSu
        public int? IdNhanSu { get; set; }
        public CreateNhanSuDto? ThongTinNhanSu { get; set; }
    }
}
