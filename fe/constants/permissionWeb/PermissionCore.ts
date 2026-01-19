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

  //KPI
  public static readonly CoreMenuKpi = `${PermissionCoreConst.Menu}kpi`;

  public static readonly CoreMenuKpiList = `${PermissionCoreConst.Menu}kpi_list`;

  public static readonly CoreMenuKpiListRole = `${PermissionCoreConst.Menu}kpi_list_role`;
  public static readonly CoreMenuKpiListPersonal = `${PermissionCoreConst.Menu}kpi_list_personal`;
  public static readonly CoreMenuKpiListPersonalCreate = `${PermissionCoreConst.Button}kpi_list_personal_create`;
  public static readonly CoreMenuKpiListPersonalUpdate = `${PermissionCoreConst.Button}kpi_list_personal_update`;
  public static readonly CoreMenuKpiListPersonalDelete = `${PermissionCoreConst.Button}kpi_list_personal_delete`;
  public static readonly CoreMenuKpiListPersonalAction = `${PermissionCoreConst.Button}kpi_list_personal_action`;
  public static readonly CoreMenuKpiListPersonalActionScore = `${PermissionCoreConst.Button}kpi_list_personal_action_score`;
  public static readonly CoreMenuKpiListPersonalActionSyncScore = `${PermissionCoreConst.Button}kpi_list_personal_action_sync_score`;
  public static readonly CoreMenuKpiListPersonalActionSaveScore = `${PermissionCoreConst.Button}kpi_list_personal_action_save_score`;
  public static readonly CoreMenuKpiListPersonalActionPrincipalApprove = `${PermissionCoreConst.Button}kpi_list_personal_action_principal_approve`;

  public static readonly CoreMenuKpiListUnit = `${PermissionCoreConst.Menu}kpi_list_unit`;
  public static readonly CoreMenuKpiListUnitCreate = `${PermissionCoreConst.Button}kpi_list_unit_create`;
  public static readonly CoreMenuKpiListUnitUpdate = `${PermissionCoreConst.Button}kpi_list_unit_update`;
  public static readonly CoreMenuKpiListUnitDelete = `${PermissionCoreConst.Button}kpi_list_unit_delete`;
  public static readonly CoreMenuKpiListUnitAction = `${PermissionCoreConst.Button}kpi_list_unit_action`;
  public static readonly CoreMenuKpiListUnitActionApprove = `${PermissionCoreConst.Button}kpi_list_unit_action_approve`;
  public static readonly CoreMenuKpiListUnitActionScore = `${PermissionCoreConst.Button}kpi_list_unit_action_score`;
  public static readonly CoreMenuKpiListUnitActionSyncScore = `${PermissionCoreConst.Button}kpi_list_unit_action_sync_score`;
  public static readonly CoreMenuKpiListUnitActionSaveScore = `${PermissionCoreConst.Button}kpi_list_unit_action_save_score`;
  public static readonly CoreMenuKpiListUnitActionPrincipalApprove = `${PermissionCoreConst.Button}kpi_list_unit_action_principal_approve`;

  public static readonly CoreMenuKpiListSchool = `${PermissionCoreConst.Menu}kpi_list_school`;
  public static readonly CoreMenuKpiListSchoolCreate = `${PermissionCoreConst.Button}kpi_list_school_create`;
  public static readonly CoreMenuKpiListSchoolUpdate = `${PermissionCoreConst.Button}kpi_list_school_update`;
  public static readonly CoreMenuKpiListSchoolDelete = `${PermissionCoreConst.Button}kpi_list_school_delete`;
  public static readonly CoreMenuKpiListSchoolAction = `${PermissionCoreConst.Button}kpi_list_school_action`;
  public static readonly CoreMenuKpiListSchoolActionScore = `${PermissionCoreConst.Button}kpi_list_school_action_score`;
  public static readonly CoreMenuKpiListSchoolActionSyncScore = `${PermissionCoreConst.Button}kpi_list_school_action_sync_score`;
  public static readonly CoreMenuKpiListSchoolActionSaveScore = `${PermissionCoreConst.Button}kpi_list_school_action_save_score`;
  public static readonly CoreMenuKpiListSchoolActionPrincipalApprove = `${PermissionCoreConst.Button}kpi_list_school_action_principal_approve`;

  public static readonly CoreMenuKpiManage = `${PermissionCoreConst.Menu}kpi_manage`;

  public static readonly CoreMenuKpiManagePersonal = `${PermissionCoreConst.Menu}kpi_manage_personal`;
  public static readonly CoreMenuKpiManagePersonalActionSaveScore = `${PermissionCoreConst.Button}kpi_manage_personal_action_save_score`;
  public static readonly CoreMenuKpiManagePersonalAction = `${PermissionCoreConst.Button}kpi_manage_personal_action`;
  public static readonly CoreMenuKpiManagePersonalActionSendDeclared = `${PermissionCoreConst.Button}kpi_manage_personal_action_send_declared`;
  public static readonly CoreMenuKpiManagePersonalActionCancelDeclared = `${PermissionCoreConst.Button}kpi_manage_personal_action_cancel_declared`;


  public static readonly CoreMenuKpiManageUnit = `${PermissionCoreConst.Menu}kpi_manage_unit`;
  public static readonly CoreMenuKpiManageUnitCreate = `${PermissionCoreConst.Button}kpi_manage_unit_create`;
  public static readonly CoreMenuKpiManageUnitActionSaveScore = `${PermissionCoreConst.Button}kpi_manage_unit_action_save_score`;
  public static readonly CoreMenuKpiManageUnitAssign = `${PermissionCoreConst.Button}kpi_manage_unit_assign`;
  public static readonly CoreMenuKpiManageUnitAction = `${PermissionCoreConst.Button}kpi_manage_unit_action`;
  public static readonly CoreMenuKpiManageUnitActionPropose = `${PermissionCoreConst.Button}kpi_manage_unit_action_propose`;
  public static readonly CoreMenuKpiManageUnitActionCancelPropose = `${PermissionCoreConst.Button}kpi_manage_unit_action_cancel_propose`;
  public static readonly CoreMenuKpiManageUnitActionSendDeclared = `${PermissionCoreConst.Button}kpi_manage_unit_action_send_declared`;
  public static readonly CoreMenuKpiManageUnitActionCancelDeclared = `${PermissionCoreConst.Button}kpi_manage_unit_action_cancel_declared`;

  public static readonly CoreMenuKpiManageSchool = `${PermissionCoreConst.Menu}kpi_manage_school`;
  public static readonly CoreMenuKpiManageSchoolActionSaveScore = `${PermissionCoreConst.Button}kpi_manage_school_action_save_score`;
  public static readonly CoreMenuKpiManageSchoolAssign = `${PermissionCoreConst.Button}kpi_manage_school_assign`;
  public static readonly CoreMenuKpiManageSchoolAction = `${PermissionCoreConst.Button}kpi_manage_school_action`;
  public static readonly CoreMenuKpiManageSchoolActionSendDeclared = `${PermissionCoreConst.Button}kpi_manage_school_action_send_declared`;
  public static readonly CoreMenuKpiManageSchoolActionCancelDeclared = `${PermissionCoreConst.Button}kpi_manage_school_action_cancel_declared`;
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
