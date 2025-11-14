using D.Auth.Domain.Dtos.Permission;

namespace d.Shared.Permission.Permission
{
    public static class PermissionConfig
    {
        public static readonly Dictionary<string, CreatePermissionRequestDto> CoreConfigs = new()
        {
            #region User
            #region Phân quyền website
            { PermissionKeys.UserMenuPermission,new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserMenuPermission, PermissionName = "Phân quyền website", ParentKey = null } },
            { PermissionKeys.UserTableRolePermission, new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserTableRolePermission, PermissionName = "Danh sách nhóm quyền", ParentKey = PermissionKeys.UserMenuPermission } },
            { PermissionKeys.UserButtonPermissionSetting, new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserButtonPermissionSetting, PermissionName = "Setting nhóm quyền", ParentKey = PermissionKeys.UserMenuPermission } },
            { PermissionKeys.UserButtonPermissionAdd, new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserButtonPermissionAdd, PermissionName = "Thêm nhóm quyền", ParentKey = PermissionKeys.UserButtonPermissionSetting } },
            { PermissionKeys.UserButtonRolePermissionUpdate,  new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserButtonRolePermissionUpdate, PermissionName = "Chỉnh sửa nhóm quyền", ParentKey = PermissionKeys.UserButtonPermissionSetting } },
            { PermissionKeys.UserButtonRolePermissionDelete, new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserButtonRolePermissionDelete, PermissionName = "Xóa nhóm quyền", ParentKey = PermissionKeys.UserButtonPermissionSetting } },
            #endregion
            #region Quản lý tài khoản
            { PermissionKeys.UserMenuAccountManager, new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserMenuAccountManager, PermissionName = "Quản lý tài khoản", ParentKey = null } },
            { PermissionKeys.UserButtonAccountManagerAdd,  new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserButtonAccountManagerAdd, PermissionName = "Thêm tài khoản", ParentKey = PermissionKeys.UserMenuAccountManager } },
            { PermissionKeys.UserTableAccountManager, new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserTableAccountManager, PermissionName = "Danh sách tài khoản", ParentKey = PermissionKeys.UserMenuAccountManager } },
            { PermissionKeys.UserButtonAccountManagerUpdatePermission, new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserButtonAccountManagerUpdatePermission, PermissionName = "Cập nhật quyền", ParentKey = PermissionKeys.UserMenuAccountManager } },
            { PermissionKeys.UserButtonAccountManagerUpdate, new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserButtonAccountManagerUpdate, PermissionName = "Cập nhật tài khoản", ParentKey = PermissionKeys.UserMenuAccountManager } },
            { PermissionKeys.UserButtonAccountManagerLock, new CreatePermissionRequestDto {  PermissonKey = PermissionKeys.UserButtonAccountManagerLock, PermissionName = "Khóa tài khoản", ParentKey = PermissionKeys.UserMenuAccountManager } },
            #endregion
            #endregion
            #region Hrm nhân sự
            { PermissionKeys.CoreMenuNhanSu, new CreatePermissionRequestDto { PermissonKey = PermissionKeys.CoreMenuNhanSu, PermissionName = "Quản lý nhân sự", ParentKey = null } },
            { PermissionKeys.CoreButtonCreateNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionKeys.CoreButtonCreateNhanSu, PermissionName = "Thêm nhân sự", ParentKey = PermissionKeys.CoreMenuNhanSu } },
            { PermissionKeys.CoreButtonUpdateNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionKeys.CoreButtonUpdateNhanSu, PermissionName = "Cập nhật nhân sự", ParentKey = PermissionKeys.CoreMenuNhanSu } },
            { PermissionKeys.CoreButtonDeleteNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionKeys.CoreButtonDeleteNhanSu, PermissionName = "Xóa nhân sự", ParentKey = PermissionKeys.CoreMenuNhanSu } },
            #endregion

            #region Delegation - đoàn vào / ra
            { PermissionKeys.CoreMenuDelegation, new CreatePermissionRequestDto { PermissonKey= PermissionKeys.CoreMenuDelegation, PermissionName = "Quản lý đoàn vào / ra", ParentKey = null } },
            #endregion
        };
    }
}
