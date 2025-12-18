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
    public class CreateSupporter : ICommandHandler<CreateSupporterRequestDto, CreateSupporterResponseDto>
    {
        private readonly ISupporterService _service;

        public CreateSupporter(ISupporterService service)
        {
            _service = service;
        }

        public async Task<CreateSupporterResponseDto> Handle(CreateSupporterRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.CreateSupporter(request);
        }

    }
}
