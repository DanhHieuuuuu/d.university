using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmLoaiPhongBan), Schema = DbSchema.Hrm)]
    public class DmLoaiPhongBan : EntityBase
    {
        public string? MaLoaiPhongBan { get; set; }
        public string? TenLoaiPhongBan { get; set; }
    }
}
