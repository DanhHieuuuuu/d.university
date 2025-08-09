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
                .AddScoped<IStudentService, StudentService>()
                ;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IStudentRepository, StudentRepository>();
        }

        public static IServiceCollection AddAutoMapperProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingExtention>();
            });

            return services;
        }
    }
}
