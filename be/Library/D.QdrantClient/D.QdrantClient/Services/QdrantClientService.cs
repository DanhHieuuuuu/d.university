using System.Net.Http.Json;
using System.Text.Json;
using D.QdrantClient.Configs;
using D.QdrantClient.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace D.QdrantClient.Services
{
    public class QdrantClientService : IQdrantClientService
    {
        private readonly HttpClient _http;
        private readonly ILogger<QdrantClientService> _logger;
        private readonly QdrantConfig _opt;
        private readonly JsonSerializerOptions _jsonOpt = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public QdrantClientService(HttpClient http, IOptions<QdrantConfig> opt, ILogger<QdrantClientService> logger)
        {
            _http = http;
            _opt = opt.Value;
            _http.BaseAddress = new Uri(_opt.Url.TrimEnd('/'));
            _http.DefaultRequestHeaders.Add("api-key", _opt.ApiKey);
            _logger = logger;
        }

        public async Task EnsureCollectionAsync(CancellationToken ct = default)
        {

            _logger.LogError($"QdrantConfig: {JsonSerializer.Serialize(_opt)}");

            var check = await _http.GetAsync($"/collections/{_opt.Collection}", ct);
            if (check.IsSuccessStatusCode)
                return;

            var body = new { vectors = new { size = _opt.VectorSize, distance = "Cosine" } };
            var resp = await _http.PutAsJsonAsync($"/collections/{_opt.Collection}", body, ct);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpsertPointAsync(
            string id,
            float[] vector,
            object? payload = null,
            CancellationToken ct = default
        )
        {
            await UpsertPointsBatchAsync(new[] { (id, vector, payload) }, ct);
        }

        public async Task UpsertPointsBatchAsync(
            IEnumerable<(string id, float[] vector, object? payload)> points,
            CancellationToken ct = default
        )
        {
            var pointsBody = points
                .Select(p => new
                {
                    p.id,
                    p.vector,
                    p.payload,
                })
                .ToArray();
            var body = new { points = pointsBody };
            var resp = await _http.PutAsJsonAsync(
                $"/collections/{_opt.Collection}/points",
                body,
                ct
            );

            if (!resp.IsSuccessStatusCode)
            {
                var errorContent = await resp.Content.ReadAsStringAsync(ct);
                Console.WriteLine($"Qdrant Upsert Error: {resp.StatusCode}");
                Console.WriteLine($"Error Body: {errorContent}");
            }

            resp.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<QdrantSearchResult>> SearchAsync(
            float[] queryVector,
            int top = 10,
            int _offset = 0,
            double? scoreThreshold = null,
            CancellationToken ct = default
        )
        {
            var body = new
            {
                vector = queryVector,
                limit = top,
                offest = _offset,
                with_payload = true,
                score_threshold = scoreThreshold,
            };
            var resp = await _http.PostAsJsonAsync(
                $"/collections/{_opt.Collection}/points/search",
                body,
                ct
            );

            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync(ct);

            using var doc = JsonDocument.Parse(json);
            var results = new List<QdrantSearchResult>();

            if (doc.RootElement.TryGetProperty("result", out var arr))
            {
                foreach (var item in arr.EnumerateArray())
                {
                    var id = item.GetProperty("id").GetRawText().Trim('"');
                    var score = item.GetProperty("score").GetDouble();
                    Dictionary<string, object>? payload = null;

                    if (item.TryGetProperty("payload", out var p))
                        payload = JsonSerializer.Deserialize<Dictionary<string, object>>(
                            p.GetRawText(),
                            _jsonOpt
                        );

                    results.Add(
                        new QdrantSearchResult
                        {
                            Id = id,
                            Score = score,
                            Payload = payload,
                        }
                    );
                }
            }
            return results;
        }
    }
}
