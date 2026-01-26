using D.QdrantClient.Dtos;

namespace D.QdrantClient.Services
{
    public interface IQdrantClientService
    {
        Task EnsureCollectionAsync(CancellationToken ct = default);
        Task UpsertPointAsync(string id, float[] vector, object? payload = null, CancellationToken ct = default);
        Task UpsertPointsBatchAsync(IEnumerable<(string id, float[] vector, object? payload)> points, CancellationToken ct = default);
        Task<IEnumerable<QdrantSearchResult>> SearchAsync(float[] queryVector, int top = 10, int offset = 0, double? scoreThreshold = null, CancellationToken ct = default);
    }
}
