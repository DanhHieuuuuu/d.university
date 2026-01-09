using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiDonVi
{
    public class GetTrangThaiKpiTruongByKpiDonVi : IQueryHandler<GetTrangThaiKpiTruongByKpiDonViRequestDto, GetTrangThaiKpiTruongByKpiDonViResponseDto>
    {
        private readonly IKpiDonViService _service;

        public GetTrangThaiKpiTruongByKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public Task<GetTrangThaiKpiTruongByKpiDonViResponseDto> Handle(GetTrangThaiKpiTruongByKpiDonViRequestDto request, CancellationToken cancellationToken)
        {
            return _service.GetTrangThaiKpiTruongByKpiDonViAsync(request);
        }
    }
}
