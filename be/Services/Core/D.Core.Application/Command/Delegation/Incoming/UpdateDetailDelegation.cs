using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;

public class UpdateDetailDelegation: ICommandHandler<UpdateDetailDelegationsRequestDto,List<UpdateDetailDelegationResponseDto>>
{
    private readonly IDetailDelegationIncomingService _service;

    public UpdateDetailDelegation(IDetailDelegationIncomingService service)
    {
        _service = service;
    }

    public async Task<List<UpdateDetailDelegationResponseDto>> Handle(UpdateDetailDelegationsRequestDto request,CancellationToken cancellationToken)
    {
        return await _service.UpdateDetailDelegationIncoming(request.Items);
    }
}
