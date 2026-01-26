using MediatR;
using D.Core.Domain.Dtos.Delegation.Incoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;

public class ExportReportHandler: IRequestHandler<ExportReport, byte[]>
{
    private readonly IDelegationIncomingService _service;

    public ExportReportHandler(IDelegationIncomingService service)
    {
        _service = service;
    }

    public async Task<byte[]> Handle(ExportReport request,CancellationToken cancellationToken)
    {
        return await _service.ExportDelegationIncomingReport();
    }
}
