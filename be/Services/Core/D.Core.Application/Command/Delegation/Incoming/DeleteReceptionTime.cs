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
    public class DeleteReceptionTime : ICommandHandler<DeleteReceptionTimeDto>
    {
        private readonly IReceptionTimeService _service;
        public DeleteReceptionTime(IReceptionTimeService service)
        {
            _service = service;
        }
        public async Task Handle(DeleteReceptionTimeDto request, CancellationToken cancellationToken)
        {
            _service.DeleteReceptionTime(request.Id);
        }


    }
}
