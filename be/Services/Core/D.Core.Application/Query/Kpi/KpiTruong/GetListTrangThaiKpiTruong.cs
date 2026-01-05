using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiTruong
{
    public class GetListTrangThaiKpiTruong : IQueryHandler<GetTrangThaiKpiTruongDto, List<TrangThaiKpiTruongResponseDto>>
    {
        private readonly IKpiTruongService _service;

        public GetListTrangThaiKpiTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public Task<List<TrangThaiKpiTruongResponseDto>> Handle(GetTrangThaiKpiTruongDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetListTrangThai());
        }
    }
}
