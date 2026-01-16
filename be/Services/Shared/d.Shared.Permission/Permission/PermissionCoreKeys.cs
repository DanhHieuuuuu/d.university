namespace d.Shared.Permission.Permission
{
    public static class PermissionCoreKeys
    {
        #region User Management - Quản lý người dùng
        public const string UserMenuAdmin = $"{PermissionPrefixKeys.Menu}admin_permission";

        #region Phân quyền website
        public const string UserMenuPermission = $"{PermissionPrefixKeys.Menu}user_permission";
        public const string UserTableRolePermission = $"{PermissionPrefixKeys.Table}user_role_permission";
        public const string UserButtonRoleAdd = $"{PermissionPrefixKeys.Button}user_role_add";
        public const string UserButtonPermissionSettings = $"{PermissionPrefixKeys.Button}user_permission_settings";
        public const string UserButtonRoleUpdate = $"{PermissionPrefixKeys.Button}user_role_update";
        public const string UserButtonRoleDelete = $"{PermissionPrefixKeys.Button}user_role_delete";
        public const string UserButtonRolePermissionUpdate = $"{PermissionPrefixKeys.Button}user_role_permission_update";
        #endregion

        #region Quản lý tài khoản
        public const string UserMenuAccountManager = $"{PermissionPrefixKeys.Menu}account_manager";
        public const string UserButtonAccountManagerAdd = $"{PermissionPrefixKeys.Button}add_account";
        public const string UserTableAccountManager = $"{PermissionPrefixKeys.Table}account";
        public const string UserButtonAccountManagerUpdatePermission = $"{PermissionPrefixKeys.Button}update_permission_account";
        public const string UserButtonAccountManagerUpdate = $"{PermissionPrefixKeys.Button}update_account";
        public const string UserButtonAccountManagerLock = $"{PermissionPrefixKeys.Button}lock_account";
        #endregion
        
        #endregion

        #region HRM - Nhân sự
        public const string CoreMenuNhanSu = $"{PermissionPrefixKeys.Menu}hrm";
        public const string CoreButtonCreateNhanSu = $"{PermissionPrefixKeys.Button}hrm_create_nhansu";
        public const string CoreButtonUpdateNhanSu = $"{PermissionPrefixKeys.Button}hrm_update_nhansu";
        public const string CoreButtonDeleteNhanSu = $"{PermissionPrefixKeys.Button}hrm_delete_nhansu";
        #endregion

        #region Delegation - Đoàn vào / Đoàn ra
        public const string CoreMenuDelegation = $"{PermissionPrefixKeys.Menu}dio";

        public const string CoreMenuListDoanVao = $"{PermissionPrefixKeys.Menu}dio_danhsach";
        public const string CoreButtonCreateDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_create";
        public const string CoreButtonUpdateDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_update";
        public const string CoreButtonDeleteDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_delete";
        public const string CoreButtonViewDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_view";

        public const string CoreMenuXuLyDoanVao = $"{PermissionPrefixKeys.Menu}dio_xuly";
        public const string CoreButtonCreateXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_create";
        public const string CoreButtonUpdateXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_update";
        public const string CoreButtonDeleteXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_delete";
        public const string CoreButtonViewXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_view";
              
        public const string CoreMenuDepartment = $"{PermissionPrefixKeys.Menu}dio_department";
        public const string CoreButtonCreateDepartment = $"{PermissionPrefixKeys.Button}dio_department_create";
        public const string CoreButtonUpdateDepartment = $"{PermissionPrefixKeys.Button}dio_department_update";
        public const string CoreButtonDeleteDepartment = $"{PermissionPrefixKeys.Button}dio_department_delete";
        public const string CoreButtonViewDepartment = $"{PermissionPrefixKeys.Button}dio_department_view";

        public const string CoreMenuLog = $"{PermissionPrefixKeys.Menu}dio_log";
        public const string CoreTableLog = $"{PermissionPrefixKeys.Table}dio_log_table";



        #endregion

        #region Survey - Khảo sát

        #endregion
    }
}
