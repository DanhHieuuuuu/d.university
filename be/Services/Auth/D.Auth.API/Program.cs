using D.Auth.Application;
using D.Auth.Domain;
using D.Auth.Infrastructure;
using D.ControllerBases;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;

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
            var redis = ConnectionMultiplexer.Connect("localhost:6379");

            builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

            // sign in IDatabase
            builder.Services.AddScoped<IDatabase>(sp =>
            {
                var connection = sp.GetRequiredService<IConnectionMultiplexer>();
                return connection.GetDatabase();
            });

            builder.Services.AddAutoMapperProfile().AddServices().AddRepositories();
            builder.Services.AddMediatRServices();
            builder.ConfigureCors();

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
