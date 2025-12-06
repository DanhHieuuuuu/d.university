using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiCaNhan
{
    public class GetListTrangThai : IQueryHandler<GetTrangThaiDto, List<int>>
    {
        private readonly IKpiCaNhanService _service;

        public GetListTrangThai(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public Task<List<int>> Handle(GetTrangThaiDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetListTrangThai());
        }
    }
}
