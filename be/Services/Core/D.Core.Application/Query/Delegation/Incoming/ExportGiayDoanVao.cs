using D.Core.Domain.Dtos.Delegation.Incoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using MediatR;

public class ExportGiayDoanVao: IRequestHandler<ExportGiayDoanVaoDto, ExportFileDto>
{
    private readonly IDelegationIncomingService _service;

    public ExportGiayDoanVao(IDelegationIncomingService service)
    {
        _service = service;
    }

    public Task<ExportFileDto> Handle(ExportGiayDoanVaoDto request, CancellationToken cancellationToken)
    {
        return Task.FromResult( _service.ExportGiayDoanVao(request) );
    }
}
