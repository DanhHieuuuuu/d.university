using System.Reflection;
using D.Auth.Application;
using D.Auth.Domain;
using D.Auth.Infrastructure;
using D.ControllerBase;
using D.Notification.ApplicationService.Configs;
using D.S3Bucket.Configs;

namespace D.Auth.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.ConfigureTokenSwagger();
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            builder.ConfigureSumarrySwagger(xmlFilename);

            builder.ConfigureDbContext<AuthDBContext>();

            // connection redis
            builder.ConfigureRedis();
            builder.ConfigureNotification(typeof(Program).Namespace);

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
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
