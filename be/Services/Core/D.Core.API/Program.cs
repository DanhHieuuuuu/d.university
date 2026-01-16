using D.ControllerBase;
using D.Core.Application;
using D.Core.Domain;
using D.Core.Infrastructure;
using D.Notification.ApplicationService.Configs;
using D.Notification.Infrastructure.Hubs;
using D.S3Bucket.Configs;
using Serilog;
using System.Reflection;

namespace D.Core.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ServiceName", "CoreAPI")
                .CreateLogger();

            try
            {
                builder.Host.UseSerilog();

                builder.Services.AddControllers();

                builder.ConfigureTokenSwagger();
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                builder.ConfigureSumarrySwagger(xmlFilename);

                builder.ConfigureDbContext<CoreDBContext>();

                // connection redis
                builder.ConfigureRedis();
                builder.ConfigureNotification(typeof(Program).Namespace);

                builder.Services.AddAutoMapperProfile().AddServices().AddRepositories();
                builder.Services.AddMediatRServices();
                builder.ConfigureCors();
                builder.ConfigureS3();
                builder.Services.AddHttpClient();

                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "D.Auth API V1");
                        c.RoutePrefix = "swagger";
                    });
                }
                app.UseCors(ProgramBase.CorsPolicy);
                app.MapHub<NotificationHub>("/notification-hub").RequireCors(ProgramBase.SignalRCors);
                app.UseSerilogRequestLogging();

                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();
               

                Log.Information("Core started successfully.");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Core application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}