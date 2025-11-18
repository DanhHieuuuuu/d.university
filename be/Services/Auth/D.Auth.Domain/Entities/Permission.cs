using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Auth.Domain.Entities
{
    [Table(nameof(Permission), Schema = "auth")]
    public class Permission : EntityBase
    {
        [Column(nameof(PermissionKey)), MaxLength(255), Description("Mã của permisison")]
        public string? PermissionKey { get; set; }

        [Column(nameof(PermissionName)), MaxLength(255), Description("Tên của permisson")]
        public string? PermissionName { get; set; }

        [Column(nameof(ParentID)), Description("ID của permisison cha")]
        public int? ParentID { get; set; }
    }
}
