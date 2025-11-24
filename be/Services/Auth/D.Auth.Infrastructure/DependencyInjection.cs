using D.Auth.Domain;
using D.Auth.Infrastructure.Repositories;
using D.Auth.Infrastructure.Services.Abstracts;
using D.Auth.Infrastructure.Services.Implements;
using Microsoft.Extensions.DependencyInjection;

namespace D.Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                // Core Services
                .AddScoped<D.Core.Infrastructure.ServiceUnitOfWork>()
                .AddScoped<D.Core.Infrastructure.Services.File.Abstracts.IFileService, D.Core.Infrastructure.Services.File.Implements.FileService>()
                // Auth Services
                .AddScoped<ServiceUnitOfWork>()
                .AddScoped<INsNhanSuService, NsNhanSuService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IPermissionService, PermissionService>()
                ;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                // Core Repositories
                .AddScoped<D.Core.Infrastructure.Repositories.File.IFileRepository, D.Core.Infrastructure.Repositories.File.FileRepository>()
                // Auth Repositories
                .AddScoped<INsNhanSuRepository, NsNhanSuRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IRolePermissionRepository, RolePermissionRepository>()
                .AddScoped<IUserRoleRepository, UserRoleRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IPermissionRepository, PermissionRepository>()
                ;
        }

        public static IServiceCollection AddAutoMapperProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AuthMappingExtention>();
            });

            return services;
        }
    }
}
