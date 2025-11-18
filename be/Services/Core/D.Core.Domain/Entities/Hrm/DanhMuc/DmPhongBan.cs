using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmPhongBan), Schema = DbSchema.Hrm)]
    public class DmPhongBan : EntityBase
    {
        public string? MaPhongBan { get; set; }
        public string? TenPhongBan { get; set; }
        public int? IdLoaiPhongBan { get; set; }
        public string? DiaChi { get; set; }
        public string? Hotline { get; set; }
        public string? Fax { get; set; }
        public DateTime? NgayThanhLap { get; set; }
        public bool? IsActive { get; set; } = true;
        public int? STT { get; set; }
        public string? ChucVuNguoiDaiDien { get; set; }
        public string? NguoiDaiDien { get; set; }
    }
}
