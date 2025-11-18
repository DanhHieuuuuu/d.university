using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmLoaiHopDong), Schema = DbSchema.Hrm)]
    public class DmLoaiHopDong : EntityBase
    {
        public string? MaLoaiHopDong { get; set; }
        public string? TenLoaiHopDong { get; set; }
        public string? IdBieuMau { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
