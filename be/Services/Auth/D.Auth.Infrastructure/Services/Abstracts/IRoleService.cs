using D.Auth.Domain.Dtos.Permission;
using D.Auth.Domain.Dtos.Role;
using D.DomainBase.Dto;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface IRoleService
    {
        bool CreateRole(CreateRoleRequestDto request);
        void UpdateRole(UpdateRoleDto request);
        PageResultDto<RoleResponseDto> GetAllRole(FindPagingRoleRequestDto dto);
        void UpdateRolePermission(UpdateRolePermissionDto dto);
        RoleFindByIdResponseDto FindRoleById(int id);
    }
}
