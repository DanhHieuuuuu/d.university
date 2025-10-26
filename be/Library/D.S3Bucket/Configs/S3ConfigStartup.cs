using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace D.S3Bucket.Configs
{
    public static class S3ConfigStartup
    {
        public static void ConfigureS3(this WebApplicationBuilder builder)
        {
            // Đăng ký S3Config vào DI Container
            builder.Services.Configure<S3Config>(builder.Configuration.GetSection("S3Config"));

            // Lấy cấu hình từ appsettings
            var endpoint = builder.Configuration.GetSection("S3Config:Endpoint").Value;
            var accessKey = builder.Configuration.GetSection("S3Config:AccessKey").Value;
            var secretKey = builder.Configuration.GetSection("S3Config:SecretKey").Value;
            var withSSL = builder.Configuration.GetValue<bool>("S3Config:WithSSL");

            // Đăng ký MinIO Client
            builder.Services.AddMinio(configureClient => configureClient
                .WithEndpoint(endpoint)
                .WithSSL(withSSL)
                .WithCredentials(accessKey, secretKey));

            // Đăng ký S3ManagerFile service
            builder.Services.AddScoped<IS3ManagerFile, S3ManagerFile>();
        }
    }
}
