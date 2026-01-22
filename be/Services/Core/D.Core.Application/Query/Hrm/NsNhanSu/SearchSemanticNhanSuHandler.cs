using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.SemanticSearch;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm.NsNhanSu
{
    public class SearchSemanticNhanSuHandler
        : IQueryHandler<SearchSemanticRequestDto, List<SearchSemanticResponseDto>>
    {
        private readonly INhanSuQdrantService _service;

        public SearchSemanticNhanSuHandler(INhanSuQdrantService service)
        {
            _service = service;
        }

        public async Task<List<SearchSemanticResponseDto>> Handle(
            SearchSemanticRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.SearchSemanticAsync(request, cancellationToken);
            return result;
        }
    }
}
