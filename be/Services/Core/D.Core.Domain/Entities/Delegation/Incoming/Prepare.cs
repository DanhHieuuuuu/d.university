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
    [Table(nameof(Prepare), Schema = DbSchema.Delegation)]
    public class Prepare : EntityBase
    {
        [Column("Name")]
        [Description("Tên")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("Description")]
        [Description("Mô tả")]
        [MaxLength(255)]
        public string Description { get; set; }

        [Column("Money")]
        [Description("Giá trị")]
        [MaxLength(255)]
        public decimal Money { get; set; }

        [Column("ReceptionTimeId")]
        [Description("Khóa ngoại của thời gian tiếp đón")]
        [MaxLength(255)]
        public int ReceptionTimeId { get; set; }

        [ForeignKey(nameof(ReceptionTimeId))]
        public ReceptionTime ReceptionTime { get; set; }
    }
}
