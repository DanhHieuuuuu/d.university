using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmChucVu), Schema = DbSchema.Hrm)]
    public class DmChucVu : EntityBase
    {
        public string? MaChucVu { get; set; }
        public string? TenChucVu { get; set; }

        [Precision(4, 2)]
        public decimal? HsChucVu { get; set; }

        [Precision(4, 2)]
        public decimal? HsTrachNhiem { get; set; }
    }
}
