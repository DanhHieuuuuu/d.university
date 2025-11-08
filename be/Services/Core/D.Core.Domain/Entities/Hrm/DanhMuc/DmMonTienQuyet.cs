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
    [Table(nameof(DmMonTienQuyet), Schema = DbSchema.Dt)]
    public class DmMonTienQuyet : EntityBase
    {
        public int MonHocId { get; set; }
        public int MonTienQuyetId { get; set; }

        [ForeignKey(nameof(MonHocId))]
        public virtual DmMonHoc MonHoc { get; set; }

        [ForeignKey(nameof(MonTienQuyetId))]
        public virtual DmMonHoc MonTienQuyet { get; set; }
    }
}
