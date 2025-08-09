using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace D.Auth.Application
{
    public static class MediatRServiceRegistration
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            // Đăng ký tất cả các IRequestHandler, INotificationHandler, v.v. trong assembly này
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(MediatRServiceRegistration).Assembly);
            });
            return services;
        }
    }
}
