using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiDonVi
{
    public class GetListTrangThaiKpiDonVi : IQueryHandler<GetTrangThaiKpiDonViDto, List<TrangThaiKpiDonViResponseDto>>
    {
        private readonly IKpiDonViService _service;

        public GetListTrangThaiKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public Task<List<TrangThaiKpiDonViResponseDto>> Handle(GetTrangThaiKpiDonViDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetListTrangThai());
        }
    }
}
