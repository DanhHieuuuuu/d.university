using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Delegation.Incoming
{
    [Table(nameof(DetailDelegationIncoming), Schema = DbSchema.Delegation)]

    public class DetailDelegationIncoming : EntityBase
    {
        [Column("Code")]
        [Description("Mã thành viên đoàn")]
        [MaxLength(255)]
        public string Code { get; set; }

        [Column("FirstName")]
        [Description("Họ")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Column("LastName")]
        [Description("Tên")]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Column("YearOfBirth")]
        [Description("Năm sinh")]
        public int YearOfBirth { get; set; }

        [Column("PhoneNumber")]
        [Description("Số điện thoại liên lạc")]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        [Column("Email")]
        [Description("Email")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Column("IsLeader")]
        [Description("Trưởng đoàn")]
        [MaxLength(255)]
        public bool IsLeader { get; set; }

        [Column("DelegationIncomingId")]
        [Description("Khóa ngoại của đoàn vào")]
        [MaxLength(255)]
        public int DelegationIncomingId { get; set; }

        [ForeignKey(nameof(DelegationIncomingId))]
        public DelegationIncoming DelegationIncoming { get; set; }
    }
}
