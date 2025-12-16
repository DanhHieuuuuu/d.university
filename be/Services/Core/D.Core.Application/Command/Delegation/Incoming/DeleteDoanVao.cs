using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Repositories.Delegation.Incoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Delegation.Incoming
{
    public class DeleteDoanVao : ICommandHandler<DeleteDelegationIncomingDto>   
    {
        private readonly IDelegationIncomingService _service;
        public DeleteDoanVao(IDelegationIncomingService service) 
        { 
            _service = service;
        }
        public async Task Handle (DeleteDelegationIncomingDto request, CancellationToken cancellationToken)
        {
            _service.DeleteDoanVao(request.Id);
        }


    }
}
