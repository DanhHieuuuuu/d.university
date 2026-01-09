using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan
{
    public class DmPhongBanByKpiRoleResponseDto
    {
        public int Id { get; set; }
        public string? MaPhongBan { get; set; }
        public string? TenPhongBan { get; set; }
        public string? LoaiPhongBan { get; set; }
        public string? DiaChi { get; set; }
        public string? Hotline { get; set; }
        public string? Fax { get; set; }
        public DateTime? NgayThanhLap { get; set; }
        public int? STT { get; set; }
        public string? ChucVuNguoiDaiDien { get; set; }
        public string? NguoiDaiDien { get; set; }
    }
}
