export class PermissionCoreConst {
  private static readonly Menu = 'menu_';
  private static readonly Tab = 'tab_';
  private static readonly Table = 'table_';
  private static readonly Button = 'btn_';

  // Tổng quan
  public static readonly CoreMenuDashboard = `${PermissionCoreConst.Menu}core_dashboad`;

  //  Nhân sự
  public static readonly CoreMenuNhanSu = `${PermissionCoreConst.Menu}hrm`;
  public static readonly CoreButtonCreateNhanSu = `${PermissionCoreConst.Button}hrm_create_nhansu`;
  public static readonly CoreButtonUpdateNhanSu = `${PermissionCoreConst.Button}hrm_update_nhansu`;
  public static readonly CoreButtonDeleteNhanSu = `${PermissionCoreConst.Button}hrm_delete_nhansu`;

  // Đoàn vào / ra
  public static readonly CoreMenuDelegation = `${PermissionCoreConst.Menu}delegation`;

  // Admin
  public static readonly UserMenuAdmin = `${PermissionCoreConst.Menu}admin_permission`;

  //Phân quyền Website
  public static readonly UserMenuPermission = `${PermissionCoreConst.Menu}user_permission`;
  public static readonly UserTableRolePermission = `${PermissionCoreConst.Table}user_role_permission`;
  public static readonly UserButtonRoleAdd = `${PermissionCoreConst.Button}user_role_add`;
  public static readonly UserButtonPermissionSettings = `${PermissionCoreConst.Button}user_permission_settings`;
  public static readonly UserButtonRolePermissionUpdate = `${PermissionCoreConst.Button}user_role_permission_update`;
  public static readonly UserButtonRoleUpdate = `${PermissionCoreConst.Button}user_role_update`;
  public static readonly UserButtonRoleDelete = `${PermissionCoreConst.Button}user_role_delete`;

  // Quản lý tài khoản
  public static readonly UserMenuAccountManager = `${PermissionCoreConst.Menu}account_manager`;
  public static readonly UserButtonAccountManagerAdd = `${PermissionCoreConst.Button}add_account`;
  public static readonly UserTableAccountManager = `${PermissionCoreConst.Table}account`;
  public static readonly UserButtonAccountManagerUpdatePermission = `${PermissionCoreConst.Button}update_permission_account`;
  public static readonly UserButtonAccountManagerUpdate = `${PermissionCoreConst.Button}update_account`;
  public static readonly UserButtonAccountManagerLock = `${PermissionCoreConst.Button}lock_account`;
}
