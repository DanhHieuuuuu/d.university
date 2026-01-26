using MediatR;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;

public class GetAllCreatedDateHandler
    : IRequestHandler<GetAllCreatedDateRequestDto, List<DateOptionDto>>
{
    private readonly ILogReceptionTimeService _service;

    public GetAllCreatedDateHandler(ILogReceptionTimeService service)
    {
        _service = service;
    }

    public Task<List<DateOptionDto>> Handle(
        GetAllCreatedDateRequestDto request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _service.GetAllCreatedDate()
        );
    }
}
