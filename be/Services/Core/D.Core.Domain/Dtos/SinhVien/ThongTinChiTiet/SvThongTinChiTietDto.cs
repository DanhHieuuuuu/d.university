using D.DomainBase.Common;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.SinhVien.ThongTinChiTiet
{
    public class SvThongTinChiTietRequestDto : IQuery<SvThongTinChiTietResponseDto>
    {
        private string? _mssv;

        [FromQuery(Name = "mssv")]
        public string? Mssv
        {
            get => _mssv;
            set => _mssv = value?.Trim();
        }
    }

    public class SvThongTinChiTietResponseDto
    {
        public ThongTinSinhVienDto SinhVien { get; set; }
        public ThongTinKhoaDto Khoa { get; set; }
        public ThongTinNganhDto Nganh { get; set; }
        public ThongTinChuyenNganhDto? ChuyenNganh { get; set; }
        public List<ChuongTrinhKhungHocKyDto> ChuongTrinhKhung { get; set; } = new();
        public List<DiemCacKyDto> DiemCacKy { get; set; } = new();
        public ThongTinHocVuDto ThongTinHocVu { get; set; }
        public List<QuyDinhThangDiemDto> QuyDinhThangDiem { get; set; } = new();
    }

    public class ThongTinSinhVienDto
    {
        public string? MaSinhVien { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public string? KhoaHoc { get; set; }
        public int? HocKyHienTai { get; set; }
        public string? TinhTrang { get; set; }
    }

    public class ThongTinKhoaDto
    {
        public string? MaKhoa { get; set; }
        public string? TenKhoa { get; set; }
        public string? MoTa { get; set; }
    }

    public class ThongTinNganhDto
    {
        public string? MaNganh { get; set; }
        public string? TenNganh { get; set; }
        public int? SoTinChiToiThieu { get; set; }
        public string? ThoiGianDaoTao { get; set; }
        public string? MoTa { get; set; }
    }

    public class ThongTinChuyenNganhDto
    {
        public string? MaChuyenNganh { get; set; }
        public string? TenChuyenNganh { get; set; }
        public string? MoTa { get; set; }
    }

    public class ChuongTrinhKhungHocKyDto
    {
        public int HocKy { get; set; }
        public List<MonHocKhungDto> MonHoc { get; set; } = new();
    }

    public class MonHocKhungDto
    {
        public string? MaMon { get; set; }
        public string? TenMon { get; set; }
        public int SoTinChi { get; set; }
        public string? Loai { get; set; }
    }

    public class DiemCacKyDto
    {
        public int HocKy { get; set; }
        public string? NamHoc { get; set; }
        public List<DiemMonDto> DiemMon { get; set; } = new();
        public decimal DiemTrungBinhHocKy { get; set; }
        public decimal DiemTrungBinhTichLuy { get; set; }
        public int SoTinChiDat { get; set; }
        public int SoTinChiTichLuy { get; set; }
        public string? XepLoaiHocKy { get; set; }
    }

    public class DiemMonDto
    {
        public string? MaMon { get; set; }
        public string? TenMon { get; set; }
        public decimal DiemQuaTrinh { get; set; }
        public decimal DiemCuoiKy { get; set; }
        public decimal DiemTongKet { get; set; }
        public decimal DiemHe4 { get; set; }
        public string? DiemChu { get; set; }
        public string? KetQua { get; set; }
    }

    public class ThongTinHocVuDto
    {
        public decimal GpaHienTai { get; set; }
        public string? XepLoaiHocLuc { get; set; }
        public int SoMonNo { get; set; }
        public bool CanhBaoHocVu { get; set; }
    }

    public class QuyDinhThangDiemDto
    {
        public string? DiemChu { get; set; }
        public decimal DiemSoMin { get; set; }
        public decimal DiemSoMax { get; set; }
        public decimal DiemHe4 { get; set; }
    }
}
