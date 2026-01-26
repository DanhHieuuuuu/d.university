using System.Text;
using System.Text.Json;
using D.Embedding.Jina.Configs;
using Microsoft.Extensions.Options;

namespace D.Embedding.Jina.Services
{
    internal static class JinaTask
    {
        public const string RetrievalQuery = "retrieval.query";
        public const string RetrievalPassage = "retrieval.passage";
        public const string TextMatching = "text-matching";
    }

    public class JinaEmbeddingService : IJinaEmbeddingService
    {
        private readonly HttpClient _http;
        private readonly JinaConfig _opt;
        private readonly JsonSerializerOptions _jsonOpt = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public JinaEmbeddingService(HttpClient http, IOptions<JinaConfig> opt)
        {
            _http = http;
            _opt = opt.Value;
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _opt.ApiKey);
        }

        public Task<float[]> EmbedQueryAsync(string text, CancellationToken ct = default) =>
            EmbedSingleAsync(text, JinaTask.RetrievalQuery, ct);

        public Task<float[][]> EmbedQueriesAsync(
            IEnumerable<string> texts,
            CancellationToken ct = default
        ) => EmbedBatchAsync(texts, JinaTask.RetrievalQuery, ct);

        public Task<float[]> EmbedPassageAsync(string text, CancellationToken ct = default) =>
            EmbedSingleAsync(text, JinaTask.RetrievalPassage, ct);

        public Task<float[][]> EmbedPassagesAsync(
            IEnumerable<string> texts,
            CancellationToken ct = default
        ) => EmbedBatchAsync(texts, JinaTask.RetrievalPassage, ct);

        public Task<float[]> EmbedForMatchingAsync(string text, CancellationToken ct = default) =>
            EmbedSingleAsync(text, JinaTask.TextMatching, ct);

        public Task<float[][]> EmbedForMatchingBatchAsync(
            IEnumerable<string> texts,
            CancellationToken ct = default
        ) => EmbedBatchAsync(texts, JinaTask.TextMatching, ct);

        private async Task<float[]> EmbedSingleAsync(string text, string task, CancellationToken ct)
        {
            var vectors = await EmbedBatchAsync(new[] { text }, task, ct);
            return vectors[0];
        }

        private async Task<float[][]> EmbedBatchAsync(
            IEnumerable<string> texts,
            string task,
            CancellationToken ct
        )
        {
            var input = texts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            if (input.Length == 0)
                throw new ArgumentException("Input texts cannot be empty");

            var request = new
            {
                model = _opt.Model,
                task,
                input,
            };

            using var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            using var response = await _http.PostAsync(_opt.Url, content, ct);
            response.EnsureSuccessStatusCode();

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync(ct));

            return doc
                .RootElement.GetProperty("data")
                .EnumerateArray()
                .Select(x =>
                {
                    var emb = x.GetProperty("embedding")
                        .EnumerateArray()
                        .Select(v => (float)v.GetDouble())
                        .ToArray();

                    return emb;
                })
                .ToArray();
        }
    }
}
