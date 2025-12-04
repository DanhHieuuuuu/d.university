using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiCaNhanService
    {
        PageResultDto<KpiCaNhanDto> GetAllKpiCaNhan(FilterKpiCaNhanDto dto);
        void CreateKpiCaNhan(CreateKpiCaNhanDto dto);
        void DeleteKpiCaNhan(int id);
        void UpdateKpiCaNhan(UpdateKpiCaNhanDto dto);
        Task<List<KpiCaNhanDto>> UpdateTrangThaiKpiCaNhan(UpdateTrangThaiKpiDto dto);
        Task<PageResultDto<KpiCaNhanDto>> FindPagingKpiCaNhanAsync(FilterKpiCaNhanDto dto);
        void UpdateKetQuaThucTe(UpdateKpiThucTeKpiCaNhanListDto dto);
        List<int> GetListTrangThai();
    }
}
