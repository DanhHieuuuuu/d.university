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
            { PermissionCoreKeys.CoreMenuHrm, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreMenuHrm, PermissionName = "Quản lý nhân sự", ParentKey = null } },

            { PermissionCoreKeys.CoreMenuHrmDanhSach, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreMenuHrmDanhSach, PermissionName = "Quản lý hồ sơ nhân sự", ParentKey = PermissionCoreKeys.CoreMenuHrm } },
            { PermissionCoreKeys.CoreButtonSyncNhanSu, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreButtonSyncNhanSu, PermissionName = "Đồng bộ nhân sự với Qdrant", ParentKey = PermissionCoreKeys.CoreMenuHrmDanhSach } },
            { PermissionCoreKeys.CoreButtonCreateNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateNhanSu, PermissionName = "Thêm nhân sự", ParentKey = PermissionCoreKeys.CoreMenuHrmDanhSach } },
            { PermissionCoreKeys.CoreButtonUpdateNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonUpdateNhanSu, PermissionName = "Cập nhật nhân sự", ParentKey = PermissionCoreKeys.CoreMenuHrmDanhSach } },
            { PermissionCoreKeys.CoreButtonDeleteNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonDeleteNhanSu, PermissionName = "Xóa nhân sự", ParentKey = PermissionCoreKeys.CoreMenuHrmDanhSach } },
            { PermissionCoreKeys.CoreButtonViewNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonViewNhanSu, PermissionName = "Xem hồ sơ nhân sự", ParentKey = PermissionCoreKeys.CoreMenuHrmDanhSach } },
            // { PermissionCoreKeys.CoreButtonExportCVNhanSu, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonExportCVNhanSu, PermissionName = "Xuất mẫu hồ sơ nhân sự", ParentKey = PermissionCoreKeys.CoreButtonViewNhanSu } },

            { PermissionCoreKeys.CoreMenuHrmContract, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreMenuHrmContract, PermissionName = "Quản lý hợp đồng tuyển dụng", ParentKey = PermissionCoreKeys.CoreMenuHrm } },
            { PermissionCoreKeys.CoreTableHrmContract, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreTableHrmContract, PermissionName = "Xem danh sách các hợp đồng tuyển dụng", ParentKey = PermissionCoreKeys.CoreMenuHrmContract } },
            { PermissionCoreKeys.CoreButtonCreateHrmContract, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreButtonCreateHrmContract, PermissionName = "Thêm mới hợp đồng", ParentKey = PermissionCoreKeys.CoreMenuHrmContract } },

            { PermissionCoreKeys.CoreMenuHrmDecision, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreMenuHrmDecision, PermissionName = "Quản lý các quyết định", ParentKey = PermissionCoreKeys.CoreMenuHrm } },
            // { PermissionCoreKeys.CoreButtonCreateHrmDecision, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreButtonCreateHrmDecision, PermissionName = "Tạo mới các quyết định", ParentKey = PermissionCoreKeys.CoreMenuHrmDecision } },
            // { PermissionCoreKeys.CoreButtonUpdateHrmDecisionStatus, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.CoreButtonUpdateHrmDecisionStatus, PermissionName = "Phê duyệt / Từ chối các quyết định", ParentKey = PermissionCoreKeys.CoreMenuHrmDecision } },

            #endregion

            #region Delegation - đoàn vào / ra
            { PermissionCoreKeys.CoreMenuDelegation, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuDelegation, PermissionName = "Quản lý đoàn vào / ra", ParentKey = null } },

            { PermissionCoreKeys.CoreMenuListDoanVao,new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuListDoanVao, PermissionName = "Danh sách đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuDelegation } },
            { PermissionCoreKeys.CoreButtonCreateDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateDoanVao, PermissionName = "Thêm đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreButtonUpdateDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonUpdateDoanVao, PermissionName = "Cập nhật đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreButtonDeleteDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonDeleteDoanVao, PermissionName = "Xoá đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreButtonViewDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonViewDoanVao, PermissionName = "Chi tiết đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreButtonCreateTimeDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateTimeDoanVao, PermissionName = "Thêm thời gian tiếp đoàn", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreButtonDeXuatDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonDeXuatDoanVao, PermissionName = "Đề xuất đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreButtonSearchDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonSearchDoanVao, PermissionName = "Tìm kiếm đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },
            { PermissionCoreKeys.CoreTableListDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreTableListDoanVao, PermissionName = "Bảng danh sách đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuListDoanVao } },

            { PermissionCoreKeys.CoreMenuXuLyDoanVao,new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuXuLyDoanVao, PermissionName = "Xử lý đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuDelegation } },
            { PermissionCoreKeys.CoreButtonCreateXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateXuLyDoanVao, PermissionName = "Thêm đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonUpdateXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonUpdateXuLyDoanVao, PermissionName = "Cập nhật đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonDeleteXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonDeleteXuLyDoanVao, PermissionName = "Xoá đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonViewXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonViewXuLyDoanVao, PermissionName = "Chi tiết đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonBaoCaoXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonBaoCaoXuLyDoanVao, PermissionName = "Báo cáo", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonTiepDoanXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonTiepDoanXuLyDoanVao, PermissionName = "Tiếp đoàn", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonSearchXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonSearchXuLyDoanVao, PermissionName = "Tìm kiếm đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonTableXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonTableXuLyDoanVao, PermissionName = "Bảng xử lý đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },
            { PermissionCoreKeys.CoreButtonExportXuLyDoanVao, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonExportXuLyDoanVao, PermissionName = "Xuất báo cáo hoàn thành", ParentKey = PermissionCoreKeys.CoreMenuXuLyDoanVao } },

            { PermissionCoreKeys.CoreMenuDepartment,new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuDepartment, PermissionName = "Phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDelegation } },
            { PermissionCoreKeys.CoreButtonCreateDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateDepartment, PermissionName = "Thêm phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },
            { PermissionCoreKeys.CoreButtonUpdateDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonUpdateDepartment, PermissionName = "Cập nhật phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },
            { PermissionCoreKeys.CoreButtonDeleteDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonDeleteDepartment, PermissionName = "Xoá phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },
            { PermissionCoreKeys.CoreButtonViewDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonViewDepartment, PermissionName = "Chi tiết phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },
            { PermissionCoreKeys.CoreButtonCreateSupporterDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonCreateSupporterDepartment, PermissionName = "Thêm nhân sự hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },
            { PermissionCoreKeys.CoreTableDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreTableDepartment, PermissionName = "Bảng phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },
            { PermissionCoreKeys.CoreButtonSearchDepartment, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreButtonSearchDepartment, PermissionName = "Tìm kiếm phòng ban hỗ trợ", ParentKey = PermissionCoreKeys.CoreMenuDepartment } },

            { PermissionCoreKeys.CoreMenuLog,new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuLog, PermissionName = "Nhật ký đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuDelegation } },
            { PermissionCoreKeys.CoreTableLog, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreTableLog, PermissionName = "Bảng nhật ký đoàn vào", ParentKey = PermissionCoreKeys.CoreMenuLog } },

            #endregion

            #region Kpi
            { PermissionCoreKeys.CoreMenuKpi, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpi, PermissionName = "KPI", ParentKey = null } },
            { PermissionCoreKeys.CoreMenuKpiList, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiList, PermissionName = "Danh sách KPI", ParentKey = PermissionCoreKeys.CoreMenuKpi } },
            { PermissionCoreKeys.CoreMenuKpiManage, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManage, PermissionName = "Quản lý KPI", ParentKey = PermissionCoreKeys.CoreMenuKpi } },

            { PermissionCoreKeys.CoreMenuKpiListRole, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListRole, PermissionName = "Danh sách KPI Role", ParentKey = PermissionCoreKeys.CoreMenuKpiList }},
            { PermissionCoreKeys.CoreMenuKpiListPersonal, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListPersonal, PermissionName = "Danh sách KPI cá nhân", ParentKey = PermissionCoreKeys.CoreMenuKpiList }},
            { PermissionCoreKeys.CoreMenuKpiListPersonalCreate, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListPersonalCreate, PermissionName = "Tạo KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListPersonal }},
            { PermissionCoreKeys.CoreMenuKpiListPersonalUpdate, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListPersonalUpdate, PermissionName = "Sửa KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListPersonal }},
            { PermissionCoreKeys.CoreMenuKpiListPersonalDelete, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListPersonalDelete, PermissionName = "Xóa KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListPersonal }},
            { PermissionCoreKeys.CoreMenuKpiListPersonalAction, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListPersonalAction, PermissionName = "Action", ParentKey = PermissionCoreKeys.CoreMenuKpiListPersonal }},
            { PermissionCoreKeys.CoreMenuKpiListPersonalActionScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListPersonalActionScore, PermissionName = "Chấm KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListPersonalAction } },
            { PermissionCoreKeys.CoreMenuKpiListPersonalActionSyncScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListPersonalActionSyncScore, PermissionName = "Đồng bộ kết quả", ParentKey = PermissionCoreKeys.CoreMenuKpiListPersonalAction }},
            { PermissionCoreKeys.CoreMenuKpiListPersonalActionSaveScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListPersonalActionSaveScore, PermissionName = "Lưu kết quả", ParentKey = PermissionCoreKeys.CoreMenuKpiListPersonalAction }},
            { PermissionCoreKeys.CoreMenuKpiListPersonalActionPrincipalApprove, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListPersonalActionPrincipalApprove, PermissionName = "Phê duyệt kết quả chấm", ParentKey = PermissionCoreKeys.CoreMenuKpiListPersonalAction }},

            { PermissionCoreKeys.CoreMenuKpiListUnit, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnit, PermissionName = "Danh sách KPI đơn vị", ParentKey = PermissionCoreKeys.CoreMenuKpiList } },
            { PermissionCoreKeys.CoreMenuKpiListUnitCreate, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnitCreate, PermissionName = "Tạo KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListUnit } },
            { PermissionCoreKeys.CoreMenuKpiListUnitUpdate, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnitUpdate, PermissionName = "Sửa KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListUnit } },
            { PermissionCoreKeys.CoreMenuKpiListUnitDelete, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnitDelete, PermissionName = "Xóa KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListUnit } },
            { PermissionCoreKeys.CoreMenuKpiListUnitAction, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnitAction, PermissionName = "Action", ParentKey = PermissionCoreKeys.CoreMenuKpiListUnit } },
            { PermissionCoreKeys.CoreMenuKpiListUnitActionApprove, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnitActionApprove, PermissionName = "Phê duyệt", ParentKey = PermissionCoreKeys.CoreMenuKpiListUnitAction } },
            { PermissionCoreKeys.CoreMenuKpiListUnitActionScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnitActionScore, PermissionName = "Chấm KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListUnitAction } },
            { PermissionCoreKeys.CoreMenuKpiListUnitActionSyncScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnitActionSyncScore, PermissionName = "Đồng bộ kết quả", ParentKey = PermissionCoreKeys.CoreMenuKpiListUnitAction } },
            { PermissionCoreKeys.CoreMenuKpiListUnitActionSaveScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnitActionSaveScore, PermissionName = "Lưu kết quả", ParentKey = PermissionCoreKeys.CoreMenuKpiListUnitAction } },
            { PermissionCoreKeys.CoreMenuKpiListUnitActionPrincipalApprove, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListUnitActionPrincipalApprove, PermissionName = "Phê duyệt kết quả chấm", ParentKey = PermissionCoreKeys.CoreMenuKpiListUnitAction } },

            { PermissionCoreKeys.CoreMenuKpiListSchool, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListSchool, PermissionName = "Danh sách KPI trường", ParentKey = PermissionCoreKeys.CoreMenuKpiList } },
            { PermissionCoreKeys.CoreMenuKpiListSchoolCreate, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListSchoolCreate, PermissionName = "Tạo KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListSchool } },
            { PermissionCoreKeys.CoreMenuKpiListSchoolUpdate, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListSchoolUpdate, PermissionName = "Sửa KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListSchool } },
            { PermissionCoreKeys.CoreMenuKpiListSchoolDelete, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListSchoolDelete, PermissionName = "Xóa KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListSchool } },
            { PermissionCoreKeys.CoreMenuKpiListSchoolAction, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListSchoolAction, PermissionName = "Action", ParentKey = PermissionCoreKeys.CoreMenuKpiListSchool } },
            { PermissionCoreKeys.CoreMenuKpiListSchoolActionScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListSchoolActionScore, PermissionName = "Chấm KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiListSchoolAction } },
            { PermissionCoreKeys.CoreMenuKpiListSchoolActionSyncScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListSchoolActionSyncScore, PermissionName = "Đồng bộ kết quả", ParentKey = PermissionCoreKeys.CoreMenuKpiListSchoolAction } },
            { PermissionCoreKeys.CoreMenuKpiListSchoolActionSaveScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListSchoolActionSaveScore, PermissionName = "Lưu kết quả", ParentKey = PermissionCoreKeys.CoreMenuKpiListSchoolAction } },
            { PermissionCoreKeys.CoreMenuKpiListSchoolActionPrincipalApprove, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiListSchoolActionPrincipalApprove, PermissionName = "Phê duyệt kết quả chấm", ParentKey = PermissionCoreKeys.CoreMenuKpiListSchoolAction } },

            { PermissionCoreKeys.CoreMenuKpiManagePersonal, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManagePersonal, PermissionName = "Kê khai KPI cá nhân", ParentKey = PermissionCoreKeys.CoreMenuKpiManage }},
            { PermissionCoreKeys.CoreMenuKpiManagePersonalAction, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManagePersonalAction, PermissionName = "Action", ParentKey = PermissionCoreKeys.CoreMenuKpiManagePersonal }},
            { PermissionCoreKeys.CoreMenuKpiManagePersonalActionSaveScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManagePersonalActionSaveScore, PermissionName = "Lưu kết quả", ParentKey = PermissionCoreKeys.CoreMenuKpiManagePersonalAction }},
            { PermissionCoreKeys.CoreMenuKpiManagePersonalActionSendDeclared, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManagePersonalActionSendDeclared, PermissionName = "Gửi chấm", ParentKey = PermissionCoreKeys.CoreMenuKpiManagePersonalAction }},
            { PermissionCoreKeys.CoreMenuKpiManagePersonalActionCancelDeclared, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManagePersonalActionCancelDeclared, PermissionName = "Hủy gửi chấm", ParentKey = PermissionCoreKeys.CoreMenuKpiManagePersonalAction }},

            { PermissionCoreKeys.CoreMenuKpiManageUnit, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageUnit, PermissionName = "Kê khai KPI đơn vị", ParentKey = PermissionCoreKeys.CoreMenuKpiManage }},
            { PermissionCoreKeys.CoreMenuKpiManageUnitAction, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageUnitAction, PermissionName = "Action", ParentKey = PermissionCoreKeys.CoreMenuKpiManageUnit }},
            { PermissionCoreKeys.CoreMenuKpiManageUnitActionSaveScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageUnitActionSaveScore, PermissionName = "Lưu kết quả", ParentKey = PermissionCoreKeys.CoreMenuKpiManageUnitAction }},
            { PermissionCoreKeys.CoreMenuKpiManageUnitActionSendDeclared, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageUnitActionSendDeclared, PermissionName = "Gửi chấm", ParentKey = PermissionCoreKeys.CoreMenuKpiManageUnitAction }},
            { PermissionCoreKeys.CoreMenuKpiManageUnitActionCancelDeclared, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageUnitActionCancelDeclared, PermissionName = "Hủy gửi chấm", ParentKey = PermissionCoreKeys.CoreMenuKpiManageUnitAction }},
            { PermissionCoreKeys.CoreMenuKpiManageUnitCreate, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageUnitCreate, PermissionName = "Tạo mới", ParentKey = PermissionCoreKeys.CoreMenuKpiManageUnit }},
            { PermissionCoreKeys.CoreMenuKpiManageUnitAssign, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageUnitAssign, PermissionName = "Giao KPI", ParentKey = PermissionCoreKeys.CoreMenuKpiManageUnit }},
            { PermissionCoreKeys.CoreMenuKpiManageUnitActionPropose, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageUnitActionPropose, PermissionName = "Đề xuất", ParentKey = PermissionCoreKeys.CoreMenuKpiManageUnitAction }},
            { PermissionCoreKeys.CoreMenuKpiManageUnitActionCancelPropose, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageUnitActionCancelPropose, PermissionName = "Hủy Đề xuất", ParentKey = PermissionCoreKeys.CoreMenuKpiManageUnitAction }},

            { PermissionCoreKeys.CoreMenuKpiManageSchool, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageSchool, PermissionName = "Kê khai KPI trường", ParentKey = PermissionCoreKeys.CoreMenuKpiManage }},
            { PermissionCoreKeys.CoreMenuKpiManageSchoolAction, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageSchoolAction, PermissionName = "Action", ParentKey = PermissionCoreKeys.CoreMenuKpiManageSchool }},
            { PermissionCoreKeys.CoreMenuKpiManageSchoolActionSaveScore, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageSchoolActionSaveScore, PermissionName = "Lưu kết quả", ParentKey = PermissionCoreKeys.CoreMenuKpiManageSchoolAction }},
            { PermissionCoreKeys.CoreMenuKpiManageSchoolActionSendDeclared, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageSchoolActionSendDeclared, PermissionName = "Gửi chấm", ParentKey = PermissionCoreKeys.CoreMenuKpiManageSchoolAction }},
            { PermissionCoreKeys.CoreMenuKpiManageSchoolActionCancelDeclared, new CreatePermissionRequestDto { PermissonKey= PermissionCoreKeys.CoreMenuKpiManageSchoolActionCancelDeclared, PermissionName = "Hủy gửi chấm", ParentKey = PermissionCoreKeys.CoreMenuKpiManageSchoolAction }},

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
            { PermissionCoreKeys.SurveyButtonAIReportGenerate, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonAIReportGenerate, PermissionName = "Đánh giá bằng AI", ParentKey = PermissionCoreKeys.SurveyMenuReport } },

            { PermissionCoreKeys.SurveyMenuLogging, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyMenuLogging, PermissionName = "Lịch sử hoạt động", ParentKey = PermissionCoreKeys.CoreMenuKhaoSat } },
            { PermissionCoreKeys.SurveyMenuReport, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyMenuReport, PermissionName = "Báo cáo khảo sát", ParentKey = PermissionCoreKeys.CoreMenuKhaoSat } },
            { PermissionCoreKeys.SurveyButtonReportGenerate, new CreatePermissionRequestDto { PermissonKey = PermissionCoreKeys.SurveyButtonReportGenerate, PermissionName = "Tạo báo cáo", ParentKey = PermissionCoreKeys.SurveyMenuReport } },
            
            #endregion
        };
    }
}
