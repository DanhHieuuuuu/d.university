using D.Auth.Domain.Dtos.Permission;

namespace d.Shared.Permission.Permission
{
    public static class PermissionConfig
    {
        public static readonly Dictionary<string, CreatePermissionRequestDto> CoreConfigs = new()
        {
            #region Hrm nhân sự
            { PermissionKeys.CoreMenuNhanSu, new CreatePermissionRequestDto { PermissonKey = PermissionKeys.CoreMenuNhanSu, PermissionName = "Quản lý nhân sự", ParentKey = null} },
            { PermissionKeys.CoreButtonCreateNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionKeys.CoreButtonCreateNhanSu, PermissionName = "Thêm nhân sự", ParentKey = PermissionKeys.CoreMenuNhanSu } },
            #endregion
        };
    }
}
