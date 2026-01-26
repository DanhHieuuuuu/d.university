using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.HopDong
{
    public class CreateHopDongDto : ICommand
    {
        public string? SoHopDong { get; set; }

        // Nếu nhân sự đã tồn tại trong db thì chỉ cần truyền IdNhanSu
        public int? IdNhanSu { get; set; }
        // Nếu nhân sự chưa tồn tại trong db thì truyền ThongTinNhanSu

        public CreateNhanSuDto? ThongTinNhanSu { get; set; }

        public int? IdLoaiHopDong { get; set; }
        public DateTime? NgayKyKet { get; set; }
        public DateTime? NgayBatDauThuViec { get; set; }
        public DateTime? NgayKetThucThuViec { get; set; }
        public DateTime? HopDongCoThoiHanTuNgay { get; set; }
        public DateTime? HopDongCoThoiHanDenNgay { get; set; }

        // Chi tiết hợp đồng
        public string? MaNhanSu { get; set; }
        public string? MaSoThue { get; set; }
        public string? TenNganHang1 { get; set; }
        public string? Atm1 { get; set; }
        public string? TenNganHang2 { get; set; }
        public string? Atm2 { get; set; }
        public int? LuongCoBan { get; set; }
        public string? GhiChu { get; set; }

        // Quyết định tiếp nhận nhân sự
        public int? IdToBoMon { get; set; }
        public int? IdPhongBan { get; set; }
        public int IdChucVu { get; set; }
    }
}
