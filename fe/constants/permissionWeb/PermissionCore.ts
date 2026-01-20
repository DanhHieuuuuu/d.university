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
  public static readonly CoreMenuDelegation = `${PermissionCoreConst.Menu}dio`;

  public static readonly CoreMenuListDoanVao = `${PermissionCoreConst.Menu}dio_danhsach`;
  public static readonly CoreButtonCreateDoanVao = `${PermissionCoreConst.Button}dio_danhsach_create`;
  public static readonly CoreButtonUpdateDoanVao = `${PermissionCoreConst.Button}dio_danhsach_update`;
  public static readonly CoreButtonDeleteDoanVao = `${PermissionCoreConst.Button}dio_danhsach_delete`;
  public static readonly CoreButtonViewDoanVao = `${PermissionCoreConst.Button}dio_danhsach_view`;
  public static readonly CoreButtonCreateTimeDoanVao = `${PermissionCoreConst.Button}dio_danhsach_create_time`;
  public static readonly CoreButtonDeXuatDoanVao = `${PermissionCoreConst.Button}dio_danhsach_dexuat`;
  public static readonly CoreButtonSearchDoanVao = `${PermissionCoreConst.Button}dio_danhsach_search`;
  public static readonly CoreTableListDoanVao = `${PermissionCoreConst.Table}dio_danhsach_table_list`;
  
  public static readonly CoreMenuXuLyDoanVao = `${PermissionCoreConst.Menu}dio_xuly`;
  public static readonly CoreButtonCreateXuLyDoanVao = `${PermissionCoreConst.Button}dio_xuly_create`;
  public static readonly CoreButtonUpdateXuLyDoanVao = `${PermissionCoreConst.Button}dio_xuly_update`;
  public static readonly CoreButtonDeleteXuLyDoanVao = `${PermissionCoreConst.Button}dio_xuly_delete`;
  public static readonly CoreButtonViewXuLyDoanVao = `${PermissionCoreConst.Button}dio_xuly_view`;
  public static readonly CoreButtonBaoCaoXuLyDoanVao = `${PermissionCoreConst.Button}dio_xuly_baocao`;
  public static readonly CoreButtonPheDuyetXuLyDoanVao = `${PermissionCoreConst.Button}dio_xuly_pheduyet`;
  public static readonly CoreButtonTiepDoanXuLyDoanVao = `${PermissionCoreConst.Button}dio_xuly_tiepdoan`;
  public static readonly CoreButtonSearchXuLyDoanVao = `${PermissionCoreConst.Button}dio_xuly_search`;
  public static readonly CoreButtonTableXuLyDoanVao = `${PermissionCoreConst.Table}dio_xuly_table`;


  public static readonly CoreMenuDepartment = `${PermissionCoreConst.Menu}dio_department`;
  public static readonly CoreButtonCreateDepartment = `${PermissionCoreConst.Button}dio_department_create`;
  public static readonly CoreButtonUpdateDepartment = `${PermissionCoreConst.Button}dio_department_update`;
  public static readonly CoreButtonDeleteDepartment = `${PermissionCoreConst.Button}dio_department_delete`;
  public static readonly CoreButtonViewDepartment = `${PermissionCoreConst.Button}dio_department_view`;
  public static readonly CoreButtonCreateSupporterDepartment = `${PermissionCoreConst.Button}dio_department_create_support`;
  public static readonly CoreTableDepartment = `${PermissionCoreConst.Table}dio_department_table`;
  public static readonly CoreButtonSearchDepartment = `${PermissionCoreConst.Button}dio_department_search`;

  public static readonly CoreMenuLog = `${PermissionCoreConst.Menu}dio_log`;
  public static readonly CoreTableLog = `${PermissionCoreConst.Table}dio_log_table`;

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
