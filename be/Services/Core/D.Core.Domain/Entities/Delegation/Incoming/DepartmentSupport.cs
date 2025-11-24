using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Delegation.Incoming
{
    [Table(nameof(DepartmentSupport), Schema = DbSchema.Delegation)]
    public class DepartmentSupport : EntityBase
    {
        [Column("DepartmentSupportId")]
        [Description("Id phòng ban hỗ trợ")]
        public int DepartmentSupportId { get; set; }

        [Column("DelegationIncomingId")]
        [Description("Id đoàn vào cần hỗ trợ")]
        public int DelegationIncomingId { get; set; }

        [ForeignKey(nameof(DelegationIncomingId))]
        public DelegationIncoming DelegationIncoming { get; set; }

        [Column("Content")]
        [Description("Nội dung hỗ trợ")]
        [MaxLength(255)]
        public string Content { get; set; }

        public ICollection<Supporter> Supporters { get; set; }
    }
}
