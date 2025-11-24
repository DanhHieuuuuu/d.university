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
    [Table(nameof(LogStatus), Schema = DbSchema.Delegation)]
    public class LogStatus : EntityBase
    {
        [Column("DelegationIncomingId")]
        [Description("Mã đoàn vào")]
        [MaxLength(255)]
        public string DelegationIncomingCode { get; set; }

        [Column("OldStatus")]
        [Description("Trạng thái cũ")]
        public int OldStatus { get; set; }

        [Column("NewStatus")]
        [Description("Trạng thái mới")]
        public int NewStatus { get; set; }

        [Column("Description")]
        [Description("Mô tả")]
        [MaxLength(255)]
        public string Description { get; set; }

        [Column("Reason")]
        [Description("Lý do")]
        [MaxLength(255)]
        public string Reason { get; set; }
    }
}
