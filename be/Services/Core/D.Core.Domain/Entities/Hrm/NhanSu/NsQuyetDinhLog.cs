using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;

namespace D.Core.Domain.Entities.Hrm.NhanSu
{
    [Table(nameof(NsQuyetDinhLog), Schema = DbSchema.Hrm)]
    public class NsQuyetDinhLog : EntityBase
    {
        public int IdQuyetDinh { get; set; }
        public int? OldStatus { get; set; }
        public int? NewStatus { get; set; }
        public string? Description { get; set; }
    }
}
