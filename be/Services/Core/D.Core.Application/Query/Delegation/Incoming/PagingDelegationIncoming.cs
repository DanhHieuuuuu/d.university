using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.File;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.Core.Infrastructure.Services.File.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.File
{
    public class PagingDelegationIncoming : IQueryHandler<FilterDelegationIncomingDto, PageResultDto<PageDelegationIncomingResultDto>>
    {
        private readonly IDelegationIncomingService _service;

        public PagingDelegationIncoming(IDelegationIncomingService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<PageDelegationIncomingResultDto>> Handle(FilterDelegationIncomingDto request, CancellationToken cancellationToken)
        {
            return _service.Paging(request);
        }
    }
}
