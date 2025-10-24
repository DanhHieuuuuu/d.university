using D.ApplicationBase;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Infrastructure.Services.Abstracts;
using D.DomainBase.Dto;

namespace D.Auth.Application.Query.Role
{
    public class GetAllRole
        : IQueryHandler<FindPagingRoleRequestDto, PageResultDto<RoleResponseDto>>
    {
        private readonly IRoleService roleService;

        public GetAllRole(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<PageResultDto<RoleResponseDto>> Handle(
            FindPagingRoleRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return roleService.GetAllRole(request);
        }
    }
}
