using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiCaNhan
{
    public class GetNhanSuKiemNhiem : IQueryHandler<GetAllNhanSuKiemNhiemRequestDto, List<GetAllNhanSuKiemNhiemResponseDto>>
    {
        private readonly IKpiCaNhanService _service;

        public GetNhanSuKiemNhiem(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public Task<List<GetAllNhanSuKiemNhiemResponseDto>> Handle(GetAllNhanSuKiemNhiemRequestDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetAllNhanSuKiemNhiem(request));
        }
    }
}
