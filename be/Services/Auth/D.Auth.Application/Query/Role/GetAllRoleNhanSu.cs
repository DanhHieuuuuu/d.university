using D.ApplicationBase;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Infrastructure.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Application.Query.Role
{
    public class GetAllRoleNhanSu : IQueryHandler<RoleRequestDto, List<string>>
    {
        private readonly IRoleService roleService;
        public GetAllRoleNhanSu(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<List<string>> Handle(RoleRequestDto request, CancellationToken cancellationToken)
        {
            return roleService.GetAllRoleNhanSu(request);
        }
    }
}
