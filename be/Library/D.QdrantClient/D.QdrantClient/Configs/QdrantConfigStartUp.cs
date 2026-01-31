using D.QdrantClient.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace D.QdrantClient.Configs
{
    public static class QdrantConfigStartUp
    {
        public static void ConfigureQdrantClient(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<QdrantConfig>(builder.Configuration.GetSection("Qdrant"));
            
            builder.Services.AddHttpClient<IQdrantClientService, QdrantClientService>();
        }
    }
}
