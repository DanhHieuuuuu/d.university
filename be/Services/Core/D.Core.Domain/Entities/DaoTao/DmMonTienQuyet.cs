using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.DaoTao
{
    [Table(nameof(DtMonTienQuyet), Schema = DbSchema.Dt)]
    public class DtMonTienQuyet : EntityBase
    {
        public int MonHocId { get; set; }
        public int MonTienQuyetId { get; set; }

        [ForeignKey(nameof(MonHocId))]
        public virtual DtMonHoc MonHoc { get; set; }

        [ForeignKey(nameof(MonTienQuyetId))]
        public virtual DtMonHoc MonTienQuyet { get; set; }
    }
}
