using D.InfrastructureBase.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace D.ControllerBases
{
    /// <summary>
    /// Cấu hình program
    /// </summary>
    public static class ProgramBase
    {
        public const string CorsPolicy = "cors_policy";

        /// <summary>
        /// Cors configuration for the application.
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    CorsPolicy,
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                );
            });
        }

        /// <summary>
        /// Token Swagger configuration for the application.
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureTokenSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = Assembly.GetEntryAssembly()?.GetName().Name, Version = "v1" });

                // Thêm cấu hình bảo mật Bearer Token
                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Nhập token vào ô bên dưới (không cần 'Bearer ' phía trước).",
                    }
                );
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                            },
                            new List<string>()
                        },
                    }
                );
            });
            builder.Services.AddHttpContextAccessor();

        }

        /// <summary>
        /// Summary Swagger configuration for the application.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="xmlFilename"></param>
        public static void ConfigureSumarrySwagger(this WebApplicationBuilder builder, string xmlFilename)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                options.IncludeXmlComments(xmlPath); // Đọc file XML để hiển thị comment
            });
        }

        public static void ConfigureDbContext<TDbContext>(this WebApplicationBuilder builder) where TDbContext : DbContext, IDbContext
        {
            // Khai báo dbcontext
            builder.Services.AddDbContext<TDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),

                        options =>
                        {
                            options.MigrationsHistoryTable(
                                "__EFMigrationsHistory"
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );
            builder.Services.AddScoped<IDbContext>(provider => provider.GetRequiredService<TDbContext>());
        }
    }
}