using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmKhoa), Schema = DbSchema.Hrm)]
    public class DmKhoa : EntityBase
    {
        public string? MaKhoa { get; set; }
        public string? TenKhoa { get; set; }
        public string? Nam { get; set; }
        public string? CachViet { get; set; }
        public bool? IsVisible { get; set; } = true;
        public int? NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
