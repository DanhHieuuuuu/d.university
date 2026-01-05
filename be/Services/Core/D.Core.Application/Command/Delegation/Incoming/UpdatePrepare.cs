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
    public class UpdatePrepare : ICommandHandler<UpdatePrepareRequestDto, List<UpdatePrepareResponseDto>>
    {
        private readonly IPrepareService _service;

        public UpdatePrepare(IPrepareService service)
        {
            _service = service;
        }

        public async Task<List<UpdatePrepareResponseDto>> Handle(UpdatePrepareRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.UpdatePrepares(request);
        }
    }
}
