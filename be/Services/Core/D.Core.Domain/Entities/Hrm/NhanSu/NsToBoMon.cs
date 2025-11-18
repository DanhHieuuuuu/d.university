using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.NhanSu
{
    [Table(nameof(NsToBoMon), Schema = DbSchema.Hrm)]
    public class NsToBoMon : EntityBase
    {
        public int? IdToBoMon { get; set; }
        public int? IdNhanSu { get; set; }
        public bool? IsToTruong { get; set; }
        public bool? IsToPho { get; set; }
    }
}
