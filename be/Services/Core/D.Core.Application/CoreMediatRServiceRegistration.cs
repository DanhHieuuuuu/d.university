using Microsoft.Extensions.DependencyInjection;

namespace D.Core.Application
{
    public static class CoreMediatRServiceRegistration
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            // Đăng ký tất cả các IRequestHandler, INotificationHandler, v.v. trong assembly này
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CoreMediatRServiceRegistration).Assembly);
            });
            return services;
        }
    }
}
