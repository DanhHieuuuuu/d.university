using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.DaoTao
{
    [Table(nameof(DtChuongTrinhKhungMon), Schema = DbSchema.Dt)]
    public class DtChuongTrinhKhungMon : EntityBase
    {
        public int ChuongTrinhKhungId { get; set; }
        public int MonHocId { get; set; }
        public string? HocKy { get; set; }
        public bool TrangThai { get; set; } = true;

        [ForeignKey(nameof(ChuongTrinhKhungId))]
        public virtual DtChuongTrinhKhung ChuongTrinhKhung { get; set; }

        [ForeignKey(nameof(MonHocId))]
        public virtual DtMonHoc MonHoc { get; set; }
    }
}
