using D.Auth.Domain.Dtos.Permission;

namespace d.Shared.Permission.Permission
{
    public static class PermissionConfig
    {
        public static readonly Dictionary<string, CreatePermissionRequestDto> CoreConfigs = new()
        {
            #region User
            { PermissionCoreKeys.UserMenuAdmin,new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserMenuAdmin, PermissionName = "Admin", ParentKey = null } },
            #region Phân quyền website
            { PermissionCoreKeys.UserMenuPermission,new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserMenuPermission, PermissionName = "Phân quyền website", ParentKey = PermissionCoreKeys.UserMenuAdmin } },
            { PermissionCoreKeys.UserTableRolePermission, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserTableRolePermission, PermissionName = "Danh sách nhóm quyền", ParentKey = PermissionCoreKeys.UserMenuPermission } },
            { PermissionCoreKeys.UserButtonPermissionSetting, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonPermissionSetting, PermissionName = "Setting nhóm quyền", ParentKey = PermissionCoreKeys.UserMenuPermission } },
            { PermissionCoreKeys.UserButtonPermissionAdd, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonPermissionAdd, PermissionName = "Thêm nhóm quyền", ParentKey = PermissionCoreKeys.UserButtonPermissionSetting } },
            { PermissionCoreKeys.UserButtonRolePermissionUpdate,  new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonRolePermissionUpdate, PermissionName = "Chỉnh sửa nhóm quyền", ParentKey = PermissionCoreKeys.UserButtonPermissionSetting } },
            { PermissionCoreKeys.UserButtonRolePermissionDelete, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonRolePermissionDelete, PermissionName = "Xóa nhóm quyền", ParentKey = PermissionCoreKeys.UserButtonPermissionSetting } },
            #endregion
            #region Quản lý tài khoản
            { PermissionCoreKeys.UserMenuAccountManager, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserMenuAccountManager, PermissionName = "Quản lý tài khoản", ParentKey = PermissionCoreKeys.UserMenuAdmin } },
            { PermissionCoreKeys.UserButtonAccountManagerAdd,  new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonAccountManagerAdd, PermissionName = "Thêm tài khoản", ParentKey = PermissionCoreKeys.UserMenuAccountManager } },
            { PermissionCoreKeys.UserTableAccountManager, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserTableAccountManager, PermissionName = "Danh sách tài khoản", ParentKey = PermissionCoreKeys.UserMenuAccountManager } },
            { PermissionCoreKeys.UserButtonAccountManagerUpdatePermission, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonAccountManagerUpdatePermission, PermissionName = "Cập nhật quyền", ParentKey = PermissionCoreKeys.UserMenuAccountManager } },
            { PermissionCoreKeys.UserButtonAccountManagerUpdate, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonAccountManagerUpdate, PermissionName = "Cập nhật tài khoản", ParentKey = PermissionCoreKeys.UserMenuAccountManager } },
            { PermissionCoreKeys.UserButtonAccountManagerLock, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonAccountManagerLock, PermissionName = "Khóa tài khoản", ParentKey = PermissionCoreKeys.UserMenuAccountManager } },
            #endregion
            #endregion
            #region Hrm nhân sự
            { PermissionCoreKeys.CoreMenuNhanSu, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreMenuNhanSu, PermissionName = "Quản lý nhân sự", ParentKey = null } },
            { PermissionCoreKeys.CoreButtonCreateNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateNhanSu, PermissionName = "Thêm nhân sự", ParentKey = PermissionCoreKeys.CoreMenuNhanSu } },
            { PermissionCoreKeys.CoreButtonUpdateNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonUpdateNhanSu, PermissionName = "Cập nhật nhân sự", ParentKey = PermissionCoreKeys.CoreMenuNhanSu } },
            { PermissionCoreKeys.CoreButtonDeleteNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonDeleteNhanSu, PermissionName = "Xóa nhân sự", ParentKey = PermissionCoreKeys.CoreMenuNhanSu } },
            #endregion

            #region Delegation - đoàn vào / ra
            { PermissionCoreKeys.CoreMenuDelegation, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuDelegation, PermissionName = "Quản lý đoàn vào / ra", ParentKey = null } },
            #endregion
        };
    }
}
