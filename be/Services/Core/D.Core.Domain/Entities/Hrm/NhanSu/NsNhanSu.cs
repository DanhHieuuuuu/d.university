using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;

namespace D.Core.Domain.Entities.Hrm.NhanSu
{
    [Table(nameof(NsNhanSu), Schema = DbSchema.Hrm)]
    public class NsNhanSu : EntityBase
    {
        public string? MaNhanSu { get; set; }

        // Thông tin cá nhân
        public string? HoDem { get; set; }
        public string? Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public bool? GioiTinh { get; set; }
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

        // Thông tin công việc
        public string? MaSoThue { get; set; }
        public string? TenNganHang1 { get; set; }
        public string? Atm1 { get; set; }
        public string? TenNganHang2 { get; set; }
        public string? Atm2 { get; set; }
        public int? HienTaiChucVu { get; set; }
        public int? HienTaiPhongBan { get; set; }
        public int? IdHopDong { get; set; }
        public bool? DaChamDutHopDong { get; set; }
        public bool? DaVeHuu { get; set; }
        public bool? IsThoiViec { get; set; }
    }
}
