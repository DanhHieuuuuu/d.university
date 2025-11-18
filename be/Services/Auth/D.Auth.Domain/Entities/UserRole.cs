using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Auth.Domain.Entities
{
    [Table(nameof(UserRole), Schema = "auth")]
    public class UserRole : EntityBase
    {
        [Column("NhanSuId"), Description("ID của nhân sự (Khóa ngoại).")]
        public int NhanSuId { get; set; }

        [Column("RoleId"), Description("ID của role (Khóa ngoại).")]
        public int RoleId { get; set; }

        [ForeignKey(nameof(NhanSuId))]
        public virtual NsNhanSu NsNhanSu { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }
    }
}
