using D.Core.Domain;
using D.Core.Infrastructure.Repositories.DaoTao;
using D.Core.Infrastructure.Repositories.Delegation.Incoming;
using D.Core.Infrastructure.Repositories.File;
using D.Core.Infrastructure.Repositories.Hrm;
using D.Core.Infrastructure.Repositories.Kpi;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.Core.Infrastructure.Repositories.Survey;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.Core.Infrastructure.Services.DaoTao.Implements;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.Core.Infrastructure.Services.Delegation.Incoming.Implements;
using D.Core.Infrastructure.Services.File.Abstracts;
using D.Core.Infrastructure.Services.File.Implements;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.Core.Infrastructure.Services.Hrm.Implements;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.Core.Infrastructure.Services.Kpi.Implements;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.Core.Infrastructure.Services.SinhVien.Implements;
using D.Core.Infrastructure.Services.Survey.Report.Abstracts;
using D.Core.Infrastructure.Services.Survey.Report.Implement;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using D.Core.Infrastructure.Services.Survey.Request.Implement;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using D.Core.Infrastructure.Services.Survey.Surveys.Implement;
using Microsoft.Extensions.DependencyInjection;

namespace D.Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ServiceUnitOfWork>()
                .AddScoped<INsNhanSuService, NsNhanSuService>()
                .AddScoped<IDmDanhMucService, DmDanhMucService>()
                .AddScoped<ISvSinhVienService, SvSinhVienService>()
                .AddScoped<IDaoTaoService, DaoTaoService>()
            #region Delegation
                .AddScoped<IDelegationIncomingService, DelegationIncomingService>()
                .AddScoped<IDepartmentSupportService, DepartmentSupportService>()
                .AddScoped<IDetailDelegationIncomingService, DetailDelegationIncomingService>()
                .AddScoped<ILogReceptionTimeService, LogReceptionTimeService>()
                .AddScoped<ILogStatusService, LogStatusService>()
                .AddScoped<IPrepareService, PrepareService>()
                .AddScoped<IReceptionTimeService, ReceptionTimeService>()
                .AddScoped<ISupporterService, SupporterService>()
                .AddScoped<IExcelService, ExcelService>()
            #endregion
            #region Survey
                .AddScoped<IRequestSurveyService, RequestSurveyService>()
                .AddScoped<ISurveyService, SurveyService>()
                .AddScoped<IReportSurveyService, ReportSurveyService>()
            #endregion
                .AddScoped<IFileService, FileService>()
            #region Kpi
                .AddScoped<IKpiRoleService, KpiRoleService>()
                .AddScoped<IKpiCaNhanService, KpiCaNhanService>()
                .AddScoped<IKpiDonViService, KpiDonViService>()
                .AddScoped<IKpiTruongService, KpiTruongService>()
                .AddScoped<IKpiTemplateService, KpiTemplateService>();
            #endregion
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IDmChucVuRepository, DmChucVuRepository>()
                .AddScoped<IDmDanTocRepository, DmDanTocRepository>()
                .AddScoped<IDmGioiTinhRepository, DmGioiTinhRepository>()
                .AddScoped<IDmLoaiHopDongRepository, DmLoaiHopDongRepository>()
                .AddScoped<IDmLoaiPhongBanRepository, DmLoaiPhongBanRepository>()
                .AddScoped<IDmPhongBanRepository, DmPhongBanRepository>()
                .AddScoped<IDmQuanHeGiaDinhRepository, DmQuanHeGiaDinhRepository>()
                .AddScoped<IDmQuocTichRepository, DmQuocTichRepository>()
                .AddScoped<IDmToBoMonRepository, DmToBoMonRepository>()
                .AddScoped<IDmTonGiaoRepository, DmTonGiaoRepository>()
                .AddScoped<IDmKhoaHocRepository, DmKhoaHocRepository>()
                .AddScoped<INsNhanSuRepository, NsNhanSuRepository>()
                .AddScoped<INsQuanHeGiaDinhRepository, NsQuanHeGiaDinhRepository>()
                .AddScoped<INsHopDongRepository, NsHopDongRepository>()
                .AddScoped<INsHopDongChiTietRepository, NsHopDongChiTietRepository>()
                .AddScoped<INsToBoMonRepository, NsToBoMonRepository>()
                .AddScoped<ISvSinhVienRepository, SvSinhVienRepository>()
            #region DaoTao
                .AddScoped<IDtKhoaRepository, DtKhoaRepository>()
                .AddScoped<IDtNganhRepository, DtNganhRepository>()
                .AddScoped<IDtChuyenNganhRepository, DtChuyenNganhRepository>()
                .AddScoped<IDtMonHocRepository, DtMonHocRepository>()
                .AddScoped<IDtMonTienQuyetRepository, DtMonTienQuyetRepository>()
                .AddScoped<IDtChuongTrinhKhungRepository, DtChuongTrinhKhungRepository>()
                .AddScoped<IDtChuongTrinhKhungMonRepository, DtChuongTrinhKhungMonRepository>()
            #endregion
            #region Delegation
                .AddScoped<IDelegationIncomingRepository, DelegationIncomingRepository>()
                .AddScoped<IDepartmentSupportRepository, DepartmentSupportRepository>()
                .AddScoped<IDetailDelegationIncomingRepository, DetailDelegationIncomingRepository>()
                .AddScoped<ILogReceptionTimeRepository, LogReceptionTimeRepository>()
                .AddScoped<ILogStatusRepository, LogStatusRepository>()
                .AddScoped<IPrepareRepository, PrepareRepository>()
                .AddScoped<IReceptionTimeRepository, ReceptionTimeRepository>()
                .AddScoped<ISupporterRepository, SupporterRepository>()
            #endregion
                .AddScoped<IFileRepository, FileRepository>()
            #region Kpi
                .AddScoped<IKpiCaNhanRepository, KpiCaNhanRepository>()
                .AddScoped<IKpiDonViRepository, KpiDonViRepository>()
                .AddScoped<IKpiLogStatusRepository, KpiLogStatusRepository>()
                .AddScoped<IKpiRoleRepository, KpiRoleRepository>()
                .AddScoped<IKpiTemplateRepository, KpiTemplateRepository>()
                .AddScoped<IKpiTruongRepository, KpiTruongRepository>()
            #endregion
            #region Survey
                .AddScoped<IKsSurveyRequestRepository, SurveyRequestRepository>()
                .AddScoped<IKsSurveyRepository, SurveyRepository>()
                .AddScoped<IKsSurveyReportRepository, SurveyReportRepository>()
                .AddScoped<IKsSurveyLogRepository, SurveyLogRepository>()
                .AddScoped<IKsSurveySubmissionRepository, SurveySubmissionRepository>()
                .AddScoped<IKsSurveySubmissionAnswerRepository, SurveySubmissionAnswerRepository>()
                .AddScoped<IKsSurveySubmissionLogRepository, SurveySubmissionLogRepository>();
            #endregion
        }

        public static IServiceCollection AddAutoMapperProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CoreMappingExtention>();
            });

            return services;
        }
    }
}
