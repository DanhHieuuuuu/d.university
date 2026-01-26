using D.Core.Domain.Dtos.Hrm.QuanHeGiaDinh;
using D.Core.Domain.Dtos.Hrm.QuaTrinhCongTac;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class NsNhanSuHoSoChiTietResponseDto 
    {
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }

        // Thông tin cá nhân
        public string? HoDem { get; set; }
        public string? Ten { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public int? GioiTinh { get; set; }
        public int? QuocTich { get; set; }
        public int? DanToc { get; set; }
        public int? TonGiao { get; set; }
        public string? NguyenQuan { get; set; }
        public string? NoiOHienTai { get; set; }
        public string? SoCccd { get; set; }
        public DateTime? NgayCapCccd { get; set; }
        public string? NoiCapCccd { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? KhanCapSoDienThoai { get; set; }
        public string? KhanCapNguoiLienHe { get; set; }
        public List<NsQuanHeGiaDinhResponseDto>? ThongTinGiaDinh { get; set; }
        public List<NsQuaTrinhCongTacResponseDto>? QuaTrinhCongTac { get; set; }

        // Thông tin thuế
        public string? MaSoThue { get; set; }
        public string? TenNganHang1 { get; set; }
        public string? Atm1 { get; set; }
        public string? TenNganHang2 { get; set; }
        public string? Atm2 { get; set; }

        // Thông tin sức khỏe
        public decimal? ChieuCao { get; set; }
        public decimal? CanNang { get; set; }
        public string? NhomMau { get; set; }

        // Thông tin công việc
        public int? HienTaiChucVu { get; set; }
        public string? TenChucVu { get; set; }
        public int? HienTaiPhongBan { get; set; }
        public string? TenPhongBan { get; set; }
        public string? Email2 { get; set; }
        public int? IdToBoMon { get; set; }

        // Thông tin học vấn
        public string? TrinhDoHocVan { get; set; }
        public string? TrinhDoNgoaiNgu { get; set; }
        public string? TenHocVi { get; set; }
        public string? TenChuyenNganhHocVi { get; set; }
        public string? TenHocHam { get; set; }
        public string? TenChuyenNganhHocHam { get; set; }
        public DateTime? NgayVaoDang { get; set; }
    }
}
