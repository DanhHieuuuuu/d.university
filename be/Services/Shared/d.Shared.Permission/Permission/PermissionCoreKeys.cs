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
        public const string CoreMenuHrm = $"{PermissionPrefixKeys.Menu}hrm";

        // Quản lý danh sách hồ sơ nhân sự
        public const string CoreMenuHrmDanhSach = $"{PermissionPrefixKeys.Menu}hrm_list";
        public const string CoreButtonSyncNhanSu = $"{PermissionPrefixKeys.Button}hrm_sync_qdrant";
        public const string CoreButtonCreateNhanSu = $"{PermissionPrefixKeys.Button}hrm_create_nhansu";
        public const string CoreButtonUpdateNhanSu = $"{PermissionPrefixKeys.Button}hrm_update_nhansu";
        public const string CoreButtonDeleteNhanSu = $"{PermissionPrefixKeys.Button}hrm_delete_nhansu";
        public const string CoreButtonViewNhanSu = $"{PermissionPrefixKeys.Button}hrm_view_nhansu";
        // public const string CoreButtonExportCVNhanSu = $"{PermissionPrefixKeys.Button}hrm_export_hoso_nhansu";

        // Quản lý hợp đồng tuyển dụng
        public const string CoreMenuHrmContract = $"{PermissionPrefixKeys.Menu}hrm_contracts";
        public const string CoreTableHrmContract = $"{PermissionPrefixKeys.Table}hrm_contracts";
        public const string CoreButtonCreateHrmContract = $"{PermissionPrefixKeys.Button}hrm_create_contract";

        // Quản lý các quyết định
        public const string CoreMenuHrmDecision = $"{PermissionPrefixKeys.Menu}hrm_decisions";
        public const string CoreButtonViewHrmDecision = $"{PermissionPrefixKeys.Menu}hrm_view_decision";
        // public const string CoreButtonCreateHrmDecision = $"{PermissionPrefixKeys.Button}hrm_create_decision";
        // public const string CoreButtonUpdateHrmDecisionStatus = $"{PermissionPrefixKeys.Button}hrm_update_status_decision";

        #endregion

        #region Delegation - Đoàn vào / Đoàn ra
        public const string CoreMenuDelegation = $"{PermissionPrefixKeys.Menu}dio";

        public const string CoreMenuListDoanVao = $"{PermissionPrefixKeys.Menu}dio_danhsach";
        public const string CoreButtonCreateDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_create";
        public const string CoreButtonUpdateDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_update";
        public const string CoreButtonDeleteDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_delete";
        public const string CoreButtonViewDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_view";
        public const string CoreButtonCreateTimeDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_create_time";
        public const string CoreButtonDeXuatDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_dexuat";
        public const string CoreButtonSearchDoanVao = $"{PermissionPrefixKeys.Button}dio_danhsach_search";
        public const string CoreTableListDoanVao = $"{PermissionPrefixKeys.Table}dio_danhsach_table_list";


        public const string CoreMenuXuLyDoanVao = $"{PermissionPrefixKeys.Menu}dio_xuly";
        public const string CoreButtonCreateXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_create";
        public const string CoreButtonUpdateXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_update";
        public const string CoreButtonDeleteXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_delete";
        public const string CoreButtonViewXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_view";
        public const string CoreButtonBaoCaoXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_baocao";
        public const string CoreButtonPheDuyetXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_pheduyet";
        public const string CoreButtonTiepDoanXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_tiepdoan";
        public const string CoreButtonSearchXuLyDoanVao = $"{PermissionPrefixKeys.Button}dio_xuly_search";
        public const string CoreButtonTableXuLyDoanVao = $"{PermissionPrefixKeys.Table}dio_xuly_table";

        public const string CoreMenuDepartment = $"{PermissionPrefixKeys.Menu}dio_department";
        public const string CoreButtonCreateDepartment = $"{PermissionPrefixKeys.Button}dio_department_create";
        public const string CoreButtonUpdateDepartment = $"{PermissionPrefixKeys.Button}dio_department_update";
        public const string CoreButtonDeleteDepartment = $"{PermissionPrefixKeys.Button}dio_department_delete";
        public const string CoreButtonViewDepartment = $"{PermissionPrefixKeys.Button}dio_department_view";
        public const string CoreButtonSearchDepartment = $"{PermissionPrefixKeys.Button}dio_department_search";
        public const string CoreButtonCreateSupporterDepartment = $"{PermissionPrefixKeys.Button}dio_department_create_support";
        public const string CoreTableDepartment = $"{PermissionPrefixKeys.Table}dio_department_table";


        public const string CoreMenuLog = $"{PermissionPrefixKeys.Menu}dio_log";
        public const string CoreTableLog = $"{PermissionPrefixKeys.Table}dio_log_table";



        #endregion

        #region Kpi
        public const string CoreMenuKpi = $"{PermissionPrefixKeys.Menu}kpi";

        public const string CoreMenuKpiList = $"{PermissionPrefixKeys.Menu}kpi_list";

        public const string CoreMenuKpiListRole = $"{PermissionPrefixKeys.Menu}kpi_list_role";
        public const string CoreMenuKpiListPersonal = $"{PermissionPrefixKeys.Menu}kpi_list_personal";
        public const string CoreMenuKpiListPersonalCreate = $"{PermissionPrefixKeys.Button}kpi_list_personal_create";
        public const string CoreMenuKpiListPersonalUpdate = $"{PermissionPrefixKeys.Button}kpi_list_personal_update";
        public const string CoreMenuKpiListPersonalDelete = $"{PermissionPrefixKeys.Button}kpi_list_personal_delete";
        public const string CoreMenuKpiListPersonalAction = $"{PermissionPrefixKeys.Button}kpi_list_personal_action";
        public const string CoreMenuKpiListPersonalActionScore = $"{PermissionPrefixKeys.Button}kpi_list_personal_action_score";
        public const string CoreMenuKpiListPersonalActionSyncScore = $"{PermissionPrefixKeys.Button}kpi_list_personal_action_sync_score";
        public const string CoreMenuKpiListPersonalActionSaveScore = $"{PermissionPrefixKeys.Button}kpi_list_personal_action_save_score";
        public const string CoreMenuKpiListPersonalActionPrincipalApprove = $"{PermissionPrefixKeys.Button}kpi_list_personal_action_principal_approve";

        public const string CoreMenuKpiListUnit = $"{PermissionPrefixKeys.Menu}kpi_list_unit";
        public const string CoreMenuKpiListUnitCreate = $"{PermissionPrefixKeys.Button}kpi_list_unit_create";
        public const string CoreMenuKpiListUnitUpdate = $"{PermissionPrefixKeys.Button}kpi_list_unit_update";
        public const string CoreMenuKpiListUnitDelete = $"{PermissionPrefixKeys.Button}kpi_list_unit_delete";
        public const string CoreMenuKpiListUnitAction = $"{PermissionPrefixKeys.Button}kpi_list_unit_action";
        public const string CoreMenuKpiListUnitActionApprove = $"{PermissionPrefixKeys.Button}kpi_list_unit_action_approve";
        public const string CoreMenuKpiListUnitActionScore = $"{PermissionPrefixKeys.Button}kpi_list_unit_action_score";
        public const string CoreMenuKpiListUnitActionSyncScore = $"{PermissionPrefixKeys.Button}kpi_list_unit_action_sync_score";
        public const string CoreMenuKpiListUnitActionSaveScore = $"{PermissionPrefixKeys.Button}kpi_list_unit_action_save_score";
        public const string CoreMenuKpiListUnitActionPrincipalApprove = $"{PermissionPrefixKeys.Button}kpi_list_unit_action_principal_approve";

        public const string CoreMenuKpiListSchool = $"{PermissionPrefixKeys.Menu}kpi_list_school";
        public const string CoreMenuKpiListSchoolCreate = $"{PermissionPrefixKeys.Button}kpi_list_school_create";
        public const string CoreMenuKpiListSchoolUpdate = $"{PermissionPrefixKeys.Button}kpi_list_school_update";
        public const string CoreMenuKpiListSchoolDelete = $"{PermissionPrefixKeys.Button}kpi_list_school_delete";
        public const string CoreMenuKpiListSchoolAction = $"{PermissionPrefixKeys.Button}kpi_list_school_action";
        public const string CoreMenuKpiListSchoolActionScore = $"{PermissionPrefixKeys.Button}kpi_list_school_action_score";
        public const string CoreMenuKpiListSchoolActionSyncScore = $"{PermissionPrefixKeys.Button}kpi_list_school_action_sync_score";
        public const string CoreMenuKpiListSchoolActionSaveScore = $"{PermissionPrefixKeys.Button}kpi_list_school_action_save_score";
        public const string CoreMenuKpiListSchoolActionPrincipalApprove = $"{PermissionPrefixKeys.Button}kpi_list_school_action_principal_approve";

        public const string CoreMenuKpiManage = $"{PermissionPrefixKeys.Menu}kpi_manage";

        public const string CoreMenuKpiManagePersonal = $"{PermissionPrefixKeys.Menu}kpi_manage_personal";
        public const string CoreMenuKpiManagePersonalActionSaveScore = $"{PermissionPrefixKeys.Button}kpi_manage_personal_action_save_score";
        public const string CoreMenuKpiManagePersonalAction = $"{PermissionPrefixKeys.Button}kpi_manage_personal_action";
        public const string CoreMenuKpiManagePersonalActionSendDeclared = $"{PermissionPrefixKeys.Button}kpi_manage_personal_action_send_declared";
        public const string CoreMenuKpiManagePersonalActionCancelDeclared = $"{PermissionPrefixKeys.Button}kpi_manage_personal_action_cancel_declared";


        public const string CoreMenuKpiManageUnit = $"{PermissionPrefixKeys.Menu}kpi_manage_unit";
        public const string CoreMenuKpiManageUnitCreate = $"{PermissionPrefixKeys.Button}kpi_manage_unit_create";
        public const string CoreMenuKpiManageUnitActionSaveScore = $"{PermissionPrefixKeys.Button}kpi_manage_unit_action_save_score";
        public const string CoreMenuKpiManageUnitAssign = $"{PermissionPrefixKeys.Button}kpi_manage_unit_assign";
        public const string CoreMenuKpiManageUnitAction = $"{PermissionPrefixKeys.Button}kpi_manage_unit_action";
        public const string CoreMenuKpiManageUnitActionPropose = $"{PermissionPrefixKeys.Button}kpi_manage_unit_action_propose";
        public const string CoreMenuKpiManageUnitActionCancelPropose = $"{PermissionPrefixKeys.Button}kpi_manage_unit_action_cancel_propose";
        public const string CoreMenuKpiManageUnitActionSendDeclared = $"{PermissionPrefixKeys.Button}kpi_manage_unit_action_send_declared";
        public const string CoreMenuKpiManageUnitActionCancelDeclared = $"{PermissionPrefixKeys.Button}kpi_manage_unit_action_cancel_declared";

        public const string CoreMenuKpiManageSchool = $"{PermissionPrefixKeys.Menu}kpi_manage_school";
        public const string CoreMenuKpiManageSchoolActionSaveScore = $"{PermissionPrefixKeys.Button}kpi_manage_school_action_save_score";
        public const string CoreMenuKpiManageSchoolAssign = $"{PermissionPrefixKeys.Button}kpi_manage_school_assign";
        public const string CoreMenuKpiManageSchoolAction = $"{PermissionPrefixKeys.Button}kpi_manage_school_action";
        public const string CoreMenuKpiManageSchoolActionSendDeclared = $"{PermissionPrefixKeys.Button}kpi_manage_school_action_send_declared";
        public const string CoreMenuKpiManageSchoolActionCancelDeclared = $"{PermissionPrefixKeys.Button}kpi_manage_school_action_cancel_declared";
        #endregion

        #region Survey - Khảo sát
        public const string CoreMenuKhaoSat = $"{PermissionPrefixKeys.Menu}khaosat";

        // Request
        public const string SurveyMenuRequest = $"{PermissionPrefixKeys.Menu}survey_request";
        public const string SurveyButtonRequestCreate = $"{PermissionPrefixKeys.Button}survey_request_create";
        public const string SurveyButtonRequestUpdate = $"{PermissionPrefixKeys.Button}survey_request_update";
        public const string SurveyButtonRequestDelete = $"{PermissionPrefixKeys.Button}survey_request_delete";
        public const string SurveyButtonRequestSubmit = $"{PermissionPrefixKeys.Button}survey_request_submit";
        public const string SurveyButtonRequestCancelSubmit = $"{PermissionPrefixKeys.Button}survey_request_cancel_submit";

        // Request management
        public const string SurveyMenuRequestApproval = $"{PermissionPrefixKeys.Menu}survey_request_approval";
        public const string SurveyButtonRequestApprove = $"{PermissionPrefixKeys.Button}survey_request_approve";
        public const string SurveyButtonRequestReject = $"{PermissionPrefixKeys.Button}survey_request_reject";

        // Survey Management
        public const string SurveyMenuManagement = $"{PermissionPrefixKeys.Menu}survey_management";
        public const string SurveyButtonSurveyOpen = $"{PermissionPrefixKeys.Button}survey_open";
        public const string SurveyButtonSurveyClose = $"{PermissionPrefixKeys.Button}survey_close";

        // Report Management
        public const string SurveyMenuReport = $"{PermissionPrefixKeys.Menu}survey_report";
        public const string SurveyButtonReportGenerate = $"{PermissionPrefixKeys.Button}survey_report_generate";
        public const string SurveyButtonAIReportGenerate = $"{PermissionPrefixKeys.Button}survey_ai_report_generate";

        public const string SurveyMenuLogging = $"{PermissionPrefixKeys.Menu}survey_logging";

        #endregion
    }
}
