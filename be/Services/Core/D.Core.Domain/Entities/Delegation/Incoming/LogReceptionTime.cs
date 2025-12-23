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
    [Table(nameof(LogReceptionTime), Schema = DbSchema.Delegation)]
    public class LogReceptionTime : EntityBase
    {
        [Column("ReceptionTimeId")]
        [Description("Id thời gian tiếp đón")]
        public int ReceptionTimeId { get; set; }

        [Column("Type")]
        [Description("Loại thay đổi")]
        [MaxLength(255)]
        public string Type { get; set; }

        [Column("Description")]
        [Description("Mô tả")]
        [MaxLength(255)]
        public string Description { get; set; }

        [Column("Reason")]
        [Description("Lý do")]
        [MaxLength(255)]
        public string Reason { get; set; }
        public string? CreatedByName { get; set; }
    }
}
