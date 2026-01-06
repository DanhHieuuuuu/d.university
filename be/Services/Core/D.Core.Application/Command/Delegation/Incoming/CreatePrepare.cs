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
    public class CreatePrepare: ICommandHandler<CreatePrepareRequestDto, List<CreatePrepareResponseDto>>
    {
         private readonly IPrepareService _service;

    public CreatePrepare(IPrepareService service)
    {
        _service = service;
    }

    public async Task<List<CreatePrepareResponseDto>> Handle(CreatePrepareRequestDto request,CancellationToken cancellationToken)
    {
        return await _service.CreatePrepareList(request);
    }
}
}
