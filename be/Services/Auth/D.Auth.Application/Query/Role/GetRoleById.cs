using D.ApplicationBase;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Query.Role
{
    public class GetRoleById : IQueryHandler<RoleFindByIdRequestDto, RoleFindByIdResponseDto>
    {
        private readonly IRoleService _roleService;

        public GetRoleById(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<RoleFindByIdResponseDto> Handle(
            RoleFindByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _roleService.FindRoleById(request.Id);
        }
    }
}
