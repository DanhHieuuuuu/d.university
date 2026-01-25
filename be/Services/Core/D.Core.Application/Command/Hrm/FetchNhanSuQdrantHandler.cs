using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.SemanticSearch;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm
{
    public class FetchNhanSuQdrantHandler : ICommandHandler<FetchNhanSuQdrantDto>
    {
        private readonly INhanSuQdrantService _service;

        public FetchNhanSuQdrantHandler(INhanSuQdrantService service)
        {
            _service = service;
        }

        public async Task Handle(FetchNhanSuQdrantDto request, CancellationToken cancellationToken)
        {
            await _service.SyncAllAsync(request, cancellationToken);
            return;
        }
    }
}
