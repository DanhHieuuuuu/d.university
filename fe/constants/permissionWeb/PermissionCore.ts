export class PermissionCoreConst {
  private static readonly Menu = 'menu_';
  private static readonly Tab = 'tab_';
  private static readonly Table = 'table_';
  private static readonly Button = 'btn_';

  // Tổng quan
  public static readonly CoreMenuDashboard = `${PermissionCoreConst.Menu}core_dashboad`;

  //  Nhân sự
  public static readonly CoreMenuHrm = `${PermissionCoreConst.Menu}hrm`;

  public static readonly CoreMenuHrmDanhSach = `${PermissionCoreConst.Menu}hrm_list`;
  public static readonly CoreButtonSyncNhanSu = `${PermissionCoreConst.Button}hrm_sync_qdrant`;
  public static readonly CoreButtonCreateNhanSu = `${PermissionCoreConst.Button}hrm_create_nhansu`;
  public static readonly CoreButtonUpdateNhanSu = `${PermissionCoreConst.Button}hrm_update_nhansu`;
  public static readonly CoreButtonDeleteNhanSu = `${PermissionCoreConst.Button}hrm_delete_nhansu`;
  public static readonly CoreButtonViewNhanSu = `${PermissionCoreConst.Button}hrm_view_nhansu`;
  public static readonly CoreButtonExportCVNhanSu = `${PermissionCoreConst.Button}hrm_export_hoso_nhansu`;

  public static readonly CoreMenuHrmContract = `${PermissionCoreConst.Menu}hrm_contracts`;
  public static readonly CoreTableHrmContract = `${PermissionCoreConst.Table}hrm_contracts`;
  public static readonly CoreButtonCreateHrmContract = `${PermissionCoreConst.Button}hrm_create_contract`;

  public static readonly CoreMenuHrmDecision = `${PermissionCoreConst.Menu}hrm_decisions`;
  public static readonly CoreButtonViewHrmDecision = `${PermissionCoreConst.Button}hrm_view_decision`;
  public static readonly CoreButtonCreateHrmDecision = `${PermissionCoreConst.Button}hrm_create_decision`;
  public static readonly CoreButtonUpdateHrmDecisionStatus = `${PermissionCoreConst.Button}hrm_update_status_decision`;

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
  public static readonly CoreButtonExportXuLyDoanVao = `${PermissionCoreConst.Button}dio_xuly_export`;

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

  // Khảo sát - Survey
  public static readonly CoreMenuKhaoSat = `${PermissionCoreConst.Menu}khaosat`;

  public static readonly SurveyMenuRequest = `${PermissionCoreConst.Menu}survey_request`;
  public static readonly SurveyButtonRequestCreate = `${PermissionCoreConst.Button}survey_request_create`;
  public static readonly SurveyButtonRequestUpdate = `${PermissionCoreConst.Button}survey_request_update`;
  public static readonly SurveyButtonRequestDelete = `${PermissionCoreConst.Button}survey_request_delete`;
  public static readonly SurveyButtonRequestSubmit = `${PermissionCoreConst.Button}survey_request_submit`;
  public static readonly SurveyButtonRequestCancelSubmit = `${PermissionCoreConst.Button}survey_request_cancel_submit`;

  public static readonly SurveyMenuRequestApproval = `${PermissionCoreConst.Menu}survey_request_approval`;
  public static readonly SurveyButtonRequestApprove = `${PermissionCoreConst.Button}survey_request_approve`;
  public static readonly SurveyButtonRequestReject = `${PermissionCoreConst.Button}survey_request_reject`;

  public static readonly SurveyMenuManagement = `${PermissionCoreConst.Menu}survey_management`;
  public static readonly SurveyButtonSurveyOpen = `${PermissionCoreConst.Button}survey_open`;
  public static readonly SurveyButtonSurveyClose = `${PermissionCoreConst.Button}survey_close`;

  public static readonly SurveyMenuReport = `${PermissionCoreConst.Menu}survey_report`;
  public static readonly SurveyButtonReportGenerate = `${PermissionCoreConst.Button}survey_report_generate`;
  public static readonly SurveyButtonAIReportGenerate = `${PermissionCoreConst.Button}survey_ai_report_generate`;

  public static readonly SurveyMenuLogging = `${PermissionCoreConst.Menu}survey_logging`;
}
