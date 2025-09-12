using D.Auth.Domain.Dtos.Permission;
using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.Role
{
    public class CreateRoleRequestDto : ICommand<bool>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<CreatePermissionRequestDto> RolePermissions { get; set; }
    }
}
