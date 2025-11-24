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
    [Table(nameof(ReceptionTime), Schema = DbSchema.Delegation)]
    public class ReceptionTime : EntityBase
    {

        [Column("StartDate")]
        [Description("Thời gian bắt đầu tiếp đón")]
        public TimeOnly StartDate { get; set; }

        [Column("EndDate")]
        [Description("Thời gian kết thúc tiếp đón")]
        public TimeOnly EndDate { get; set; }

        [Column("Date")]
        [Description("Ngày tiếp đón")]
        [MaxLength(255)]
        public DateOnly Date { get; set; }

        [Column("Content")]
        [Description("Nội dung")]
        [MaxLength(255)]
        public string Content { get; set; }

        [Column("TotalPerson")]
        [Description("Tổng số người tham gia")]
        public int TotalPerson { get; set; }

        [Column("Address")]
        [Description("Vị trí tiếp đón")]
        [MaxLength (255)]
        public string Address { get; set; }

        [Column("DelegationIncomingId")]
        [Description("Khóa ngoại của đoàn vào")]
        [MaxLength(255)]
        public int DelegationIncomingId { get; set; }

        [ForeignKey(nameof(DelegationIncomingId))]
        public DelegationIncoming DelegationIncoming { get; set; }

        public ICollection<Prepare>? Prepares { get; set; }
    }
}
