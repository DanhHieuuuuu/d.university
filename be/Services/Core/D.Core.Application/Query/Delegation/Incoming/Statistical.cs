using MediatR;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;

public class Statistical : IRequestHandler<StatisticalRequestDto, StatisticalResultDto>
{
    private readonly IDelegationIncomingService _service;

    public Statistical(
        IDelegationIncomingService service
    )
    {
        _service = service;
    }

    public Task<StatisticalResultDto> Handle(StatisticalRequestDto request, CancellationToken cancellationToken)
    {
        var result = _service.GetStatistical();
        return Task.FromResult(result);
    }
}
