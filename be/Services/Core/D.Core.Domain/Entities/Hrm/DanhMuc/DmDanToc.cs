using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmDanToc), Schema = DbSchema.Hrm)]
    public class DmDanToc : EntityBase
    {
        public string? TenDanToc { get; set; }
        public string? MaDanToc { get; set; }
    }
}
