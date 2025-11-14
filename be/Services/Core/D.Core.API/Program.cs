using System.Reflection;
using D.ControllerBase;
using D.Core.Application;
using D.Core.Domain;
using D.Core.Infrastructure;
using D.Notification.ApplicationService.Configs;
using D.S3Bucket.Configs;

namespace D.Core.API
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

            builder.ConfigureDbContext<CoreDBContext>();

            // connection redis
            builder.ConfigureRedis();
            builder.ConfigureNotification(typeof(Program).Namespace);

            builder.Services.AddAutoMapperProfile().AddServices().AddRepositories();
            builder.Services.AddMediatRServices();
            builder.ConfigureCors();
            builder.ConfigureS3();

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
