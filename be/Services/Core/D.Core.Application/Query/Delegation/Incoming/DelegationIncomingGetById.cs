using D.ApplicationBase;
using D.Core.Application.Query.File;
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
    public class DelegationIncomingGetById : IQueryHandler <DelegationIncomingGetByIdRequestDto, PageDelegationIncomingResultDto>
    {
        private readonly IDelegationIncomingService _service;

        public DelegationIncomingGetById (IDelegationIncomingService service)
        {
            _service = service;
        }
        public async Task<PageDelegationIncomingResultDto> Handle(DelegationIncomingGetByIdRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdDelegationIncoming(request.Id);
        }
    }
}
