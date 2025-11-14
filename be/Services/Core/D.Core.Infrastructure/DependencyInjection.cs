using D.Core.Domain;
using D.Core.Infrastructure.Repositories.File;
using D.Core.Infrastructure.Repositories.Hrm;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.Core.Infrastructure.Services.File.Abstracts;
using D.Core.Infrastructure.Services.File.Implements;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.Core.Infrastructure.Services.Hrm.Implements;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.Core.Infrastructure.Services.SinhVien.Implements;
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
                .AddScoped<IFileService, FileService>();
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
                .AddScoped<IDmKhoaRepository, DmKhoaRepository>()
                .AddScoped<INsNhanSuRepository, NsNhanSuRepository>()
                .AddScoped<INsQuanHeGiaDinhRepository, NsQuanHeGiaDinhRepository>()
                .AddScoped<INsHopDongRepository, NsHopDongRepository>()
                .AddScoped<INsHopDongChiTietRepository, NsHopDongChiTietRepository>()
                .AddScoped<INsToBoMonRepository, NsToBoMonRepository>()
                .AddScoped<ISvSinhVienRepository, SvSinhVienRepository>()
                .AddScoped<IFileRepository, FileRepository>();
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
