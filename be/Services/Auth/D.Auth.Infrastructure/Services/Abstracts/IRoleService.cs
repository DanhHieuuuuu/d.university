using D.Auth.Domain.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface IRoleService
    {
        bool CreateRole(CreateRoleRequestDto request);
        List<string> GetAllRoleNhanSu();
    }
}
