using D.Embedding.Jina.Configs;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace D.Embedding.Jina.Services
{
    public class JinaEmbeddingService : IJinaEmbeddingService
    {
        private readonly HttpClient _http;
        private readonly JinaConfig _opt;
        private readonly JsonSerializerOptions _jsonOpt = new() { PropertyNameCaseInsensitive = true };

        public JinaEmbeddingService(HttpClient http, IOptions<JinaConfig> opt)
        {
            _http = http;
            _opt = opt.Value;
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _opt.ApiKey);
        }

        public async Task<float[]> GetEmbeddingAsync(string text, CancellationToken ct = default)
        {
            var res = await GetEmbeddingsAsync(new[] { text }, ct);
            return res.First();
        }

        public async Task<float[][]> GetEmbeddingsAsync(IEnumerable<string> texts, CancellationToken ct = default)
        {
            var payload = new
            {
                model = _opt.Model,
                task = "text-matching",
                input = texts.ToArray()
            };

            using var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            using var resp = await _http.PostAsync(_opt.Url, content, ct);
            resp.EnsureSuccessStatusCode();
            var s = await resp.Content.ReadAsStringAsync(ct);

            using var doc = JsonDocument.Parse(s);
            var list = new List<float[]>();
            foreach (var item in doc.RootElement.GetProperty("data").EnumerateArray())
            {
                var emb = item.GetProperty("embedding")
                    .EnumerateArray()
                    .Select(x => (float)x.GetDouble())
                    .ToArray();
                list.Add(emb);
            }
            return list.ToArray();
        }
    }
}
