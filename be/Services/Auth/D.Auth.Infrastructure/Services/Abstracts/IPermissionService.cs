using D.Auth.Domain.Dtos.Permission;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface IPermissionService
    {
        List<PermissionResponseDto> GetAllPermission(PermissionRequestDto dto);
        List<PermissionTreeResponseDto> GetPermissionTree(PermissionTreeRequestDto dto);
        Task ImportPermission(ImportPermissionCommand dto);
        List<string> GetPermissionsByNhanSu(GetPermissionNhanSuDto dto);
    }
}
