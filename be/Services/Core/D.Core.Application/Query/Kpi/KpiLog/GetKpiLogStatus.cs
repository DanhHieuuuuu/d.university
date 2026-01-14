using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Kpi.KpiLogStatus;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Kpi.KpiLog
{
    public class GetKpiLogStatus : IQueryHandler<FindKpiLogStatusDto, PageResultDto<KpiLogStatusDto>>
    {
        private readonly IKpiLogStatusService _service;

        public GetKpiLogStatus(IKpiLogStatusService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<KpiLogStatusDto>> Handle(FindKpiLogStatusDto request, CancellationToken cancellationToken)
        { 
             return _service.FindKpiLogs(request);
        }

    }
}
