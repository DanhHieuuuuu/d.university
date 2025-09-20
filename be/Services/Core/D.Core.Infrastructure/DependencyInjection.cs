using D.Core.Domain;
using D.Core.Infrastructure.Repositories.Hrm;
using D.Core.Infrastructure.Repositories.SinhVien;
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
                .AddScoped<ISvSinhVienService, SvSinhVienService>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<INsNhanSuRepository, NsNhanSuRepository>()
                .AddScoped<ISvSinhVienRepository, SvSinhVienRepository>();
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
