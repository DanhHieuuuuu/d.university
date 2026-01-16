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

        #region Kpi
        public const string CoreMenuKpi = $"{PermissionPrefixKeys.Menu}kpi";
        public const string CoreMenuKpiList = $"{PermissionPrefixKeys.Menu}kpi_list";
        public const string CoreMenuKpiListPersonal = $"{PermissionPrefixKeys.Menu}kpi_list_personal";
        public const string CoreMenuKpiListPersonalCreate = $"{PermissionPrefixKeys.Button}kpi_list_personal_create";
        public const string CoreMenuKpiListPersonalUpdate = $"{PermissionPrefixKeys.Button}kpi_list_personal_update";
        public const string CoreMenuKpiListPersonalDelete = $"{PermissionPrefixKeys.Button}kpi_list_personal_delete";
        public const string CoreMenuKpiListPersonalAction = $"{PermissionPrefixKeys.Button}kpi_list_personal_action";
        public const string CoreMenuKpiListPersonalActionScore = $"{PermissionPrefixKeys.Button}kpi_list_personal_action_score";
        public const string CoreMenuKpiListPersonalActionPrincipalApprove = $"{PermissionPrefixKeys.Button}kpi_list_personal_action_principal_approve";

        public const string CoreMenuKpiListUnit = $"{PermissionPrefixKeys.Menu}kpi_list_unit";
        public const string CoreMenuKpiListUnitCreate = $"{PermissionPrefixKeys.Button}kpi_list_unit_create";
        public const string CoreMenuKpiListUnitUpdate = $"{PermissionPrefixKeys.Button}kpi_list_unit_update";
        public const string CoreMenuKpiListUnitDelete = $"{PermissionPrefixKeys.Button}kpi_list_unit_delete";
        public const string CoreMenuKpiListSchool = $"{PermissionPrefixKeys.Menu}kpi_list_school";
        public const string CoreMenuKpiListSchoolCreate = $"{PermissionPrefixKeys.Button}kpi_list_school_create";
        public const string CoreMenuKpiListSchoolUpdate = $"{PermissionPrefixKeys.Button}kpi_list_school_update";
        public const string CoreMenuKpiListSchoolDelete = $"{PermissionPrefixKeys.Button}kpi_list_school_delete";
        public const string CoreMenuKpiManage = $"{PermissionPrefixKeys.Menu}kpi_manage";
        public const string CoreMenuKpiManagePersonal = $"{PermissionPrefixKeys.Menu}kpi_manage_personal";
        public const string CoreMenuKpiManageUnit = $"{PermissionPrefixKeys.Menu}kpi_manage_unit";
        public const string CoreMenuKpiManageSchool = $"{PermissionPrefixKeys.Menu}kpi_manage_school";
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

        #endregion
    }
}
