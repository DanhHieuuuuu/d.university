using D.DomainBase.Entity;

namespace D.Auth.Domain.Entities;

public partial class NsNhanSu : EntityBase
{
    public string? MaNhanSu { get; set; }

    public string? HoDem { get; set; }

    public string? Ten { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? NoiSinh { get; set; }

    public int? GioiTinh { get; set; }

    public int? QuocTich { get; set; }

    public int? DanToc { get; set; }

    public int? TonGiao { get; set; }

    public string? NguyenQuan { get; set; }

    public string? NoiOhienTai { get; set; }

    public string? SoCccd { get; set; }

    public DateTime? NgayCapCccd { get; set; }

    public string? NoiCapCccd { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? KhanCapSoDienThoai { get; set; }

    public string? KhanCapNguoiLienHe { get; set; }

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
    public string? Email2 { get; set; }

    public string? Password { get; set; }

    public string? PasswordKey { get; set; }
    public string? ImageLink { get; set; }
    public bool? Status { get; set; }
}
