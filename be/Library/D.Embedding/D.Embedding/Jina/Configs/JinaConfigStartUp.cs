using D.Embedding.Jina.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace D.Embedding.Jina.Configs
{
    public static class JinaConfigStartup
    {
        public static void ConfigureJinaEmbedding(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JinaConfig>(builder.Configuration.GetSection("EmbeddingConfig:Jina"));

            builder.Services.AddHttpClient<IJinaEmbeddingService, JinaEmbeddingService>(client =>
            {
                //client.BaseAddress = new Uri("https://api.jina.ai");
            });
        }
    }
}
