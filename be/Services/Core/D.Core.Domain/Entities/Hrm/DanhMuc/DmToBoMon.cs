using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmToBoMon), Schema = DbSchema.Hrm)]
    public class DmToBoMon : EntityBase
    {
        public string? MaBoMon { get; set; }
        public string? TenBoMon { get; set; }
        public DateTime? NgayThanhLap { get; set; }
        public int? IdPhongBan { get; set; }
    }
}
