using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.DomainBase.Entity
{
    public class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Description("Người tạo")]
        [MaxLength(255)]
        public string CreatedBy { get; set; } = "root";

        [Description("Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Description("Người sửa")]
        [MaxLength(255)]
        public string? ModifiedBy { get; set; }

        [Description("Ngày sửa")]
        public DateTime? ModifiedDate { get; set; }

        [Description("Trạng thái xóa")]
        public bool Deleted { get; set; } = false;

        [Description("Người xóa")]
        [MaxLength(255)]
        public string? DeletedBy { get; set; }

        [Description("Ngày xóa")]
        public DateTime? DeletedDate { get; set; }
    }
}
