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
    public class NextStatus : ICommandHandler<UpdateStatusRequestDto>
    {
        private readonly IDelegationIncomingService _service;

        public NextStatus(IDelegationIncomingService service)
        {
            _service = service;
        }

        public async Task Handle(UpdateStatusRequestDto request, CancellationToken cancellationToken)
        {
            await _service.NextStatus(request);
        }
    }
}
