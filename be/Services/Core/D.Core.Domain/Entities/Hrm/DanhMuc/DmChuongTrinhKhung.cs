using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmChuongTrinhKhung), Schema = DbSchema.Dt)]
    public class DmChuongTrinhKhung : EntityBase
    {
        public string MaChuongTrinhKhung { get; set; }
        public string TenChuongTrinhKhung { get; set; }

        [Description("Khóa học")]
        public string KhoaHoc { get; set; }
        public int TongSoTinChi { get; set; }
        public string? MoTa { get; set; }
        public string? MaNganh { get; set; }
        public bool TrangThai { get; set; } = true;
        public virtual ICollection<DmChuongTrinhKhungMon> ChuongTrinhKhungMons { get; set; }
    }
}
