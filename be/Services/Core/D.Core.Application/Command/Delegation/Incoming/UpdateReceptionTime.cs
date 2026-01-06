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
    public class UpdateReceptionTime : ICommandHandler<UpdateReceptionTimesRequestDto, List<UpdateReceptionTimeResponseDto>>
    {
        private readonly IReceptionTimeService _service;

        public UpdateReceptionTime(IReceptionTimeService service)
        {
            _service = service;
        }

        public async Task<List<UpdateReceptionTimeResponseDto>> Handle(UpdateReceptionTimesRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.UpdateReceptionTimes(request.Items);
        }
    }
}
