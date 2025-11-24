using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmKhoaHoc), Schema = DbSchema.Hrm)]
    public class DmKhoaHoc : EntityBase
    {
        public string? MaKhoaHoc { get; set; }
        public string? TenKhoaHoc { get; set; }
        public string? Nam { get; set; }
        public string? CachViet { get; set; }
        public bool? IsVisible { get; set; } = true;
        public int? NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
