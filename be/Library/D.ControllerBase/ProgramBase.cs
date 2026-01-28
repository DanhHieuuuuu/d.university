using D.InfrastructureBase.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;
using System.Text;

namespace D.ControllerBase
{
    /// <summary>
    /// Cấu hình program
    /// </summary>
    public static class ProgramBase
    {
        public const string CorsPolicy = "cors_policy";
        public const string SignalRCors = "signalr_cors";

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
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    SignalRCors,
                    policy =>
                    {
                        policy
                            .WithOrigins(
                                "http://localhost:3077",
                                "https://d-university-3333.vercel.app",
                                "https://d-university-core-jk86.onrender.com",
                                "https://d-university-9zz7.onrender.com"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
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
        /// <summary>
        /// Redis configuration
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureRedis(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Redis");

            if (string.IsNullOrEmpty(connectionString))
            {
               connectionString = "localhost:6379";
            }

            var redis = ConnectionMultiplexer.Connect(connectionString);

            builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

            // sign in IDatabase
            builder.Services.AddScoped<StackExchange.Redis.IDatabase>(sp =>
            {
                var connection = sp.GetRequiredService<IConnectionMultiplexer>();
                return connection.GetDatabase();
            });
        }
        /// <summary>
        /// JWT Authentication configuration
        /// </summary>
        public static void ConfigureJwtAuthentication(this WebApplicationBuilder builder)
        {
            // Đọc secret key từ appsettings.json
            var secretKey = builder.Configuration["JwtSettings:SecretKey"];
            var issuer = builder.Configuration["JwtSettings:Issuer"];
            var audience = builder.Configuration["JwtSettings:Audience"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

                // 🔥 BẮT BUỘC CHO SIGNALR
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken)
                            && path.StartsWithSegments("/notification-hub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization();
        }
    }
}