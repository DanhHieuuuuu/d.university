using D.Auth.Application;
using D.Auth.Domain;
using D.Auth.Infrastructure;
using D.ControllerBase;
using D.Notification.ApplicationService.Configs;
using D.S3Bucket.Configs;
using Serilog;
using System.Reflection;

namespace D.Auth.API
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
                .Enrich.WithProperty("ServiceName", "AuthAPI")
                .CreateLogger();

            try
            {
                builder.Host.UseSerilog();

                builder.Services.AddControllers();

                builder.ConfigureTokenSwagger();
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                builder.ConfigureSumarrySwagger(xmlFilename);

                builder.ConfigureDbContext<AuthDBContext>();

                // connection redis
                builder.ConfigureRedis();
                builder.ConfigureNotification("D.Auth.Domain");

                builder.Services.AddAutoMapperProfile().AddServices().AddRepositories();
                builder.Services.AddMediatRServices();
                builder.ConfigureCors();
                builder.ConfigureS3();

                builder.ConfigureJwtAuthentication();

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

                Log.Information("Auth started successfully.");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Auth application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}