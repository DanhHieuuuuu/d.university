using D.Auth.Domain;
using D.Auth.Infrastructure.Repositories;
using D.Auth.Infrastructure.Services.Abstracts;
using D.Auth.Infrastructure.Services.Implements;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
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
