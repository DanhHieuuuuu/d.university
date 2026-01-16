using D.Core.Domain.Dtos.Kpi.KpiCongThuc;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiCongThucService
    {
        Task<List<KpiCongThucDto>> GetAllKpiCongThuc(GetCongThucRequestDto dto);
    }
}
