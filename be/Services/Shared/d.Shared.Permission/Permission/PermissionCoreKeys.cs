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
        public const string CoreMenuDelegation = $"{PermissionPrefixKeys.Menu}delegation";
        #endregion
    }
}
