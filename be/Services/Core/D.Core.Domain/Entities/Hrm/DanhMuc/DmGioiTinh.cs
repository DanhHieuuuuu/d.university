using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmGioiTinh), Schema = DbSchema.Hrm)]
    public class DmGioiTinh : EntityBase
    {
        public string? MaGioiTinh { get; set; }
        public string? TenGioiTinh { get; set; }
    }
}
