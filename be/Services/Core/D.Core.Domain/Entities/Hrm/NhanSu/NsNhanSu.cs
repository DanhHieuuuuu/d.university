using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using Microsoft.EntityFrameworkCore;

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

        // Thông tin sức khỏe
        [Precision(4, 1)]
        public decimal? ChieuCao { get; set; }

        [Precision(4, 1)]
        public decimal? CanNang { get; set; }

        [StringLength(3)]
        public string? NhomMau { get; set; }
        public DateTime? NgayCapNhatSk { get; set; }

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

        // Thông tin đăng nhập

        [Description("Email do nhà trường cấp")]
        public string? Email2 { get; set; }
        public string? Password { get; set; }
        public string? PasswordKey { get; set; }
        public bool? Status { get; set; }
    }
}
