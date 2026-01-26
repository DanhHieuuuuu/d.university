namespace D.Embedding.Jina.Services
{
    public interface IJinaEmbeddingService
    {
        // task: text-matching => similar search
        Task<float[]> EmbedForMatchingAsync(string text, CancellationToken ct = default);
        Task<float[][]> EmbedForMatchingBatchAsync(IEnumerable<string> texts, CancellationToken ct = default);

        // task: retrieval.query => search semantic
        Task<float[]> EmbedQueryAsync(string text, CancellationToken ct = default);
        Task<float[][]> EmbedQueriesAsync(IEnumerable<string> texts, CancellationToken ct = default);
        
        // task: retrieval.passage => save data
        Task<float[]> EmbedPassageAsync(string text, CancellationToken ct = default);
        Task<float[][]> EmbedPassagesAsync(IEnumerable<string> texts, CancellationToken ct = default);
    }
}
