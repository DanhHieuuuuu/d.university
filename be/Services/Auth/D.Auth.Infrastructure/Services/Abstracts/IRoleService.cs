using D.Auth.Domain.Dtos.Role;
using D.DomainBase.Dto;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface IRoleService
    {
        PageResultDto<RoleResponseDto> GetAllRole(FindPagingRoleRequestDto dto);
        RoleFindByIdResponseDto FindRoleById(int id);
        bool CreateRole(CreateRoleRequestDto dto);
        void UpdateRole(UpdateRoleDto dto);
        void DeleteRole(int id);
        void UpdateRolePermission(UpdateRolePermissionDto dto);
    }
}
