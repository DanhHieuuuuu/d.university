using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Delegation.Incoming
{
    public class FindDelegationIncomingLog : IQueryHandler<FindDelegationIncomingLogDto, PageResultDto<ViewDelegationIncomingLogDto>>
    {
        private readonly ILogStatusService _service;

        public FindDelegationIncomingLog(ILogStatusService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<ViewDelegationIncomingLogDto>> Handle(FindDelegationIncomingLogDto request, CancellationToken cancellationToken)
        {
            return _service.FindLogDelegationIncoming(request);
        }

    }
}
