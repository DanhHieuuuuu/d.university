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
    public class CreateReceptionTime : ICommandHandler<CreateReceptionTimeRequestDto, CreateReceptionTimeResponseDto>
    {
        private readonly IReceptionTimeService _service;

        public CreateReceptionTime(IReceptionTimeService service)
        {
            _service = service;
        }

        public async Task<CreateReceptionTimeResponseDto> Handle(CreateReceptionTimeRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.CreateReceptionTime(request);
        }
    }
}
