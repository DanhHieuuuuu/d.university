namespace D.QdrantClient.Dtos
{
    public class QdrantSearchResult
    {
        public string Id { get; set; } = default!;
        public double Score { get; set; }
        public Dictionary<string, object>? Payload { get; set; }
    }
}
