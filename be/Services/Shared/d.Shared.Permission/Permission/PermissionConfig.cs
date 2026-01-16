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
            { PermissionCoreKeys.UserButtonPermissionSettings, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonPermissionSettings, PermissionName = "Settings nhóm quyền", ParentKey = PermissionCoreKeys.UserMenuPermission } },
            { PermissionCoreKeys.UserButtonRoleAdd, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonRoleAdd, PermissionName = "Thêm nhóm quyền", ParentKey = PermissionCoreKeys.UserMenuPermission } },
            { PermissionCoreKeys.UserButtonRoleUpdate,  new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonRoleUpdate, PermissionName = "Chỉnh sửa nhóm quyền", ParentKey = PermissionCoreKeys.UserButtonPermissionSettings } },
            { PermissionCoreKeys.UserButtonRolePermissionUpdate,  new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonRolePermissionUpdate, PermissionName = "Chỉnh sửa quyền của nhóm", ParentKey = PermissionCoreKeys.UserButtonPermissionSettings } },
            { PermissionCoreKeys.UserButtonRoleDelete, new CreatePermissionRequestDto {  PermissonKey = PermissionCoreKeys.UserButtonRoleDelete, PermissionName = "Xóa nhóm quyền", ParentKey = PermissionCoreKeys.UserButtonPermissionSettings } },
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

            { PermissionCoreKeys.CoreMenuListDoanVao,new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuListDoanVao, PermissionName = "Danh sách đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuDelegation } },
            { PermissionCoreKeys.CoreButtonCreateDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateDoanVao, PermissionName = "Thêm đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreButtonUpdateDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonUpdateDoanVao, PermissionName = "Cập nhật đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreButtonDeleteDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonDeleteDoanVao, PermissionName = "Xoá đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreButtonViewDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonViewDoanVao, PermissionName = "Chi tiết đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },

            { PermissionCoreKeys.CoreMenuXuLyDoanVao,new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuXuLyDoanVao, PermissionName = "Xử lý đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuDelegation } },
            { PermissionCoreKeys.CoreButtonCreateXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateXuLyDoanVao, PermissionName = "Thêm đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonUpdateXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonUpdateXuLyDoanVao, PermissionName = "Cập nhật đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonDeleteXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonDeleteXuLyDoanVao, PermissionName = "Xoá đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonViewXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonViewXuLyDoanVao, PermissionName = "Chi tiết đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },

            { PermissionCoreKeys.CoreMenuDepartment,new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuDepartment, PermissionName = "Phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDelegation } },
            { PermissionCoreKeys.CoreButtonCreateDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateDepartment, PermissionName = "Thêm phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },
            { PermissionCoreKeys.CoreButtonUpdateDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonUpdateDepartment, PermissionName = "Cập nhật phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },
            { PermissionCoreKeys.CoreButtonDeleteDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonDeleteDepartment, PermissionName = "Xoá phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },
            { PermissionCoreKeys.CoreButtonViewDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonViewDepartment, PermissionName = "Chi tiết phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },

            { PermissionCoreKeys.CoreMenuLog,new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuLog, PermissionName = "Nhật ký đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuDelegation } },
            { PermissionCoreKeys.CoreTableLog, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreTableLog, PermissionName = "Bảng nhật ký đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuLog } },

            #endregion

            #region Survey - Khảo sát

            #endregion
        };
    }
}
