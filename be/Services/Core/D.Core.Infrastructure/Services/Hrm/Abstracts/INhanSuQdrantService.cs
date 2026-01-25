using D.Core.Domain.Dtos.Hrm.SemanticSearch;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface INhanSuQdrantService
    {
        Task SyncAllAsync(FetchNhanSuQdrantDto dto, CancellationToken ct);
        Task<List<SearchSemanticResponseDto>> SearchSemanticAsync(SearchSemanticRequestDto dto, CancellationToken ct);
    }
}
