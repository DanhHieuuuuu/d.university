using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmDanToc), Schema = DbSchema.Hrm)]
    public class DmDanToc : EntityBase
    {
        public string? TenDanToc { get; set; }
        public string? MaDanToc { get; set; }
    }
}
