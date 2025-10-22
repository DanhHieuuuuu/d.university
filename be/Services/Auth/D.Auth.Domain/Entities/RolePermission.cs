using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using D.DomainBase.Entity;

namespace D.Auth.Domain.Entities
{
    [Table(nameof(RolePermission), Schema = "auth")]
    public class RolePermission : EntityBase
    {
        [Column("RoleId"), Description("Id của role.")]
        public int RoleId { get; set; }

        [Column("PermissionId"), Description("Id của permission.")]
        public int? PermissionId { get; set; }

        [Column("PermissonKey"), MaxLength(255), Description("Mã permission.")]
        public string PermissonKey { get; set; }

        [Column("PermissionName"), MaxLength(255), Description("Tên của permisson.")]
        public string PermissionName { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public Permission Permission { get; set; }
    }
}
