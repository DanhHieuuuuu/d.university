using System.Reflection;
using D.ControllerBase;
using D.Core.Application;
using D.Core.Domain;
using D.Core.Infrastructure;
using D.Notification.ApplicationService.Configs;
using Serilog;

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

                app.UseSerilogRequestLogging();

                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();

                Log.Information("Khởi động ứng dụng thành công.");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Ứng dụng lỗi không thể khởi động");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}