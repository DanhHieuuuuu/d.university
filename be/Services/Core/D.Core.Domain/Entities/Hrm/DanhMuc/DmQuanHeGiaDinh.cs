using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmQuanHeGiaDinh), Schema = DbSchema.Hrm)]
    public class DmQuanHeGiaDinh : EntityBase
    {
        public string? MaQuanHe { get; set; }
        public string? TenQuanHe { get; set; }
    }
}
