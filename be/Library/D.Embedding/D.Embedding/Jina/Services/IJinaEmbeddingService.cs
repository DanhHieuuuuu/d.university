namespace D.Embedding.Jina.Services
{
    public interface IJinaEmbeddingService
    {
        Task<float[]> GetEmbeddingAsync(string text, CancellationToken ct = default);
        Task<float[][]> GetEmbeddingsAsync(IEnumerable<string> texts, CancellationToken ct = default);
    }
}
