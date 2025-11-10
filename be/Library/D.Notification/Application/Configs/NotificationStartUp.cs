using D.Core.Domain.Shared.Constants;
using D.Notification.Application.Abstracts.Email;
using D.Notification.Application.Abstracts.Logging;
using D.Notification.Application.Abstracts.Push;
using D.Notification.Application.Abstracts.Realtime;
using D.Notification.Application.Abstracts.Sms;
using D.Notification.ApplicationService.Abstracts;
using D.Notification.ApplicationService.Implements;
using D.Notification.ApplicationService.Implements.Email;
using D.Notification.ApplicationService.Implements.Logging;
using D.Notification.ApplicationService.Implements.Push;
using D.Notification.ApplicationService.Implements.Realtime;
using D.Notification.ApplicationService.Implements.Sms;
using D.Notification.Domain.Repositories;
using D.Notification.Infrastructures.Persistence;
using D.Notification.Infrastructures.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace D.Notification.ApplicationService.Configs
{
    public static class NotificationStartUp
    {
        public static void ConfigureNotification(
            this WebApplicationBuilder builder,
            string? assemblyName
        )
        {
            builder.Services.AddDbContext<NotificationDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.Notification
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );

            // Đăng ký Config vào DI Container
            builder
                .Services.AddOptions<SmtpConfig>()
                .Bind(builder.Configuration.GetSection("Notification:SmtpConfig"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<INotificationLogRepository, NotificationLogRepository>();

            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<INotificationLogService, NotificationLogService>();

            builder.Services.AddScoped<IEmailNotification, SmtpEmailNotification>();
            builder.Services.AddScoped<ISmsNotification, TwilioSmsNotification>();
            builder.Services.AddScoped<IPushNotification, FcmPushNotification>();
            builder.Services.AddScoped<IRealtimeNotification, WebsocketNotification>();
        }
    }
}
