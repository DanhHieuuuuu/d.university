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
    [Table(nameof(Supporter), Schema = DbSchema.Delegation)]
    public class Supporter : EntityBase
    {
        [Column("SupporterId")]
        [Description("Id người hỗ trợ")]
        public int SupporterId { get; set; }

        [Column("SupporterCode")]
        [Description("Mã người hỗ trợ")]
        [MaxLength(255)]
        public string SupporterCode { get; set; }

        [Column("DepartmentSupportId")]
        [Description("Id phòng ban hỗ trợ")]
        public int DepartmentSupportId { get; set; }

        [ForeignKey(nameof(DepartmentSupportId))]
        public DepartmentSupport DepartmentSupport { get; set; }
    }
}
