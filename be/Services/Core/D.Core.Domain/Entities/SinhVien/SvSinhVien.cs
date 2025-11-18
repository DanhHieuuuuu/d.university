using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.SinhVien
{
    [Table(nameof(SvSinhVien), Schema = DbSchema.Sv)]
    public class SvSinhVien : EntityBase
    {
        public string? Mssv { get; set; }

        // Thông tin cá nhân
        public string? HoDem { get; set; }
        public string? Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public bool? GioiTinh { get; set; }
        public int? QuocTich { get; set; }
        public int? DanToc { get; set; }
        public string? SoCccd { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        // Thông tin công việc

        //public int? KhoaHoc { get; set; }
        public int? Khoa { get; set; }
        public int? ChuyenNganh { get; set; }
        public int? LopQL { get; set; }
        public int? TrangThaiHoc { get; set; }

        // Thông tin đăng nhập

        [Description("Email do nhà trường cấp")]
        public string? Email2 { get; set; }
        public string? Password { get; set; }
        public string? PasswordKey { get; set; }
    }
}
