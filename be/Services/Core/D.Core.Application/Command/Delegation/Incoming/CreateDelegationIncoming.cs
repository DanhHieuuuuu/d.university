using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Delegation.Incoming
{
    public class CreateDelegationIncoming : ICommandHandler<CreateRequestDto, CreateResponseDto>
    {
        private readonly IDelegationIncomingService _service;

        public CreateDelegationIncoming(IDelegationIncomingService service)
        {
            _service = service;
        }

        public async Task<CreateResponseDto> Handle(CreateRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.Create(request);
        }
    }
}
