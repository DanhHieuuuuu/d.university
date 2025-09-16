using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.UserRole
{
    public class CreateUserRoleDto : ICommand<bool>
    {
        public int NhanSuId { get; set; }
        public int RoleId { get; set; }
    }
}
