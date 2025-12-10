using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiDonVi
{
    public class GetListYearKpiDonVi : IQueryHandler<GetListYearRequestDto, List<GetListYearKpiDonViDto>>
    {
        private readonly IKpiDonViService _service;

        public GetListYearKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public  Task<List<GetListYearKpiDonViDto>> Handle(GetListYearRequestDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetListYear());
        }
    }
}
