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
    public class CreateReceptionTimeList
        : ICommandHandler<CreateReceptionTimeListRequestDto, List<CreateReceptionTimeResponseDto>>
    {
        private readonly IReceptionTimeService _service;

        public CreateReceptionTimeList(IReceptionTimeService service)
        {
            _service = service;
        }

        public async Task<List<CreateReceptionTimeResponseDto>> Handle(
            CreateReceptionTimeListRequestDto request,
            CancellationToken cancellationToken)
        {
            return await _service.CreateReceptionTimeList(request);
        }
    }
}

