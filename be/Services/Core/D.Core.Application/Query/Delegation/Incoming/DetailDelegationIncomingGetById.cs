using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Delegation.Incoming
{
    public class DetailDelegationIncomingGetById : IQueryHandler<DetailDelegationIncomingRequestDto, DetailDelegationIncomingResponseDto>
    {
        private readonly IDelegationIncomingService _service;

        public DetailDelegationIncomingGetById(IDelegationIncomingService service)
        {
            _service = service;
        }
        public async Task<DetailDelegationIncomingResponseDto> Handle(DetailDelegationIncomingRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdDetailDelegation(request.Id);
        }
    }
}
