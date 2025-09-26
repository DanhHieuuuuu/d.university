using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmTonGiao), Schema = DbSchema.Hrm)]
    public class DmTonGiao : EntityBase
    {
        public string? TenTonGiao { get; set; }
        public string? MaTonGiao { get; set; }
    }
}
