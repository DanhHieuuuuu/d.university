using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan
{
    public class CreateDmPhongBanDto : ICommand
    {
        public string? MaPhongBan { get; set; }
        public string? TenPhongBan { get; set; }
        public int? IdLoaiPhongBan { get; set; }
        public string? DiaChi { get; set; }
        public string? Hotline { get; set; }
        public string? Fax { get; set; }
        public DateTime? NgayThanhLap { get; set; }
        public int? STT { get; set; }
        public string? ChucVuNguoiDaiDien { get; set; }
        public string? NguoiDaiDien { get; set; }
    }
}
