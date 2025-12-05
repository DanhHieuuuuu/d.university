using D.Core.Domain.Dtos.DaoTao.MonHoc;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.DaoTao
{
    [Table(nameof(DtMonTienQuyet), Schema = DbSchema.Dt)]
    public class DtMonTienQuyet : EntityBase
    {
        public int MonHocId { get; set; }
        public int MonTienQuyetId { get; set; }
        public int LoaiDieuKien { get; set; } = 1;
        public string? GhiChu { get; set; }

        [ForeignKey(nameof(MonHocId))]
        public virtual DtMonHoc MonHoc { get; set; }

        [ForeignKey(nameof(MonTienQuyetId))]
        public virtual DtMonHoc MonTienQuyet { get; set; }
    }
}
