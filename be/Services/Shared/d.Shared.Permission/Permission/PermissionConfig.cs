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
            #endregion

            #region Survey - Khảo sát
            { PermissionCoreKeys.CoreMenuKhaoSat, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreMenuKhaoSat, PermissionName = "Quản lý khảo sát", ParentKey = null } },

            // Request 
            { PermissionCoreKeys.SurveyMenuRequest, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyMenuRequest, PermissionName = "Danh sách yêu cầu", ParentKey = PermissionCoreKeys.CoreMenuKhaoSat } },
            { PermissionCoreKeys.SurveyButtonRequestCreate, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonRequestCreate, PermissionName = "Tạo yêu cầu", ParentKey = PermissionCoreKeys.SurveyMenuRequest } },
            { PermissionCoreKeys.SurveyButtonRequestUpdate, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonRequestUpdate, PermissionName = "Sửa yêu cầu", ParentKey = PermissionCoreKeys.SurveyMenuRequest } },
            { PermissionCoreKeys.SurveyButtonRequestDelete, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonRequestDelete, PermissionName = "Xóa yêu cầu", ParentKey = PermissionCoreKeys.SurveyMenuRequest } },
            { PermissionCoreKeys.SurveyButtonRequestSubmit, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonRequestSubmit, PermissionName = "Gửi duyệt", ParentKey = PermissionCoreKeys.SurveyMenuRequest } },
            { PermissionCoreKeys.SurveyButtonRequestCancelSubmit, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonRequestCancelSubmit, PermissionName = "Hủy gửi duyệt", ParentKey = PermissionCoreKeys.SurveyMenuRequest } },

            // Request management
            { PermissionCoreKeys.SurveyMenuRequestApproval, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyMenuRequestApproval, PermissionName = "Quản lý yêu cầu", ParentKey = PermissionCoreKeys.CoreMenuKhaoSat } },
            { PermissionCoreKeys.SurveyButtonRequestApprove, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonRequestApprove, PermissionName = "Duyệt yêu cầu", ParentKey = PermissionCoreKeys.SurveyMenuRequestApproval } },
            { PermissionCoreKeys.SurveyButtonRequestReject, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonRequestReject, PermissionName = "Từ chối yêu cầu", ParentKey = PermissionCoreKeys.SurveyMenuRequestApproval } },

            // Survey Management
            { PermissionCoreKeys.SurveyMenuManagement, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyMenuManagement, PermissionName = "Danh sách khảo sát", ParentKey = PermissionCoreKeys.CoreMenuKhaoSat } },
            { PermissionCoreKeys.SurveyButtonSurveyOpen, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonSurveyOpen, PermissionName = "Mở khảo sát", ParentKey = PermissionCoreKeys.SurveyMenuManagement } },
            { PermissionCoreKeys.SurveyButtonSurveyClose, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonSurveyClose, PermissionName = "Đóng khảo sát", ParentKey = PermissionCoreKeys.SurveyMenuManagement } },

            // Report Management
            { PermissionCoreKeys.SurveyMenuReport, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyMenuReport, PermissionName = "Báo cáo khảo sát", ParentKey = PermissionCoreKeys.CoreMenuKhaoSat } },
            { PermissionCoreKeys.SurveyButtonReportGenerate, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonReportGenerate, PermissionName = "Tạo báo cáo", ParentKey = PermissionCoreKeys.SurveyMenuReport } },

            #endregion
        };
    }
}
