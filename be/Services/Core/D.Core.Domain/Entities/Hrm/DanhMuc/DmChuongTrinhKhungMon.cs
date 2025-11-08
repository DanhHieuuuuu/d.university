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
    [Table(nameof(DmChuongTrinhKhungMon), Schema = DbSchema.Dt)]
    public class DmChuongTrinhKhungMon : EntityBase
    {
        public int ChuongTrinhKhungId { get; set; }
        public int MonHocId { get; set; }
        public int? HocKy { get; set; }
        public bool TrangThai { get; set; } = true;

        [ForeignKey(nameof(ChuongTrinhKhungId))]
        public virtual DmChuongTrinhKhung ChuongTrinhKhung { get; set; }

        [ForeignKey(nameof(MonHocId))]
        public virtual DmMonHoc MonHoc { get; set; }
    }
}
