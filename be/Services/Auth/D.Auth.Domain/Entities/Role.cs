using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace D.Auth.Domain.Entities
{
    [Table(nameof(Role), Schema = "auth")]

    public class Role : EntityBase
    {
        [Column("Name"), MaxLength(255), Description("Tên của role.")]
        public string Name { get; set; }

        [Column("Description"), MaxLength(500), Description("Mô tả role.")]
        public string? Description { get; set; }

        [Column("Status"), Description("Trạng thái của role.")]
        public int Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        [JsonIgnore]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
