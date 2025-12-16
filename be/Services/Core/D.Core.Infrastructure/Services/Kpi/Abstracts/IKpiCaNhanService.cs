using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiCaNhanService
    {
        PageResultDto<KpiCaNhanDto> GetAllKpiCaNhan(FilterKpiCaNhanDto dto);
        void CreateKpiCaNhan(CreateKpiCaNhanDto dto);
        void DeleteKpiCaNhan(DeleteKpiCaNhanDto dto);
        void UpdateKpiCaNhan(UpdateKpiCaNhanDto dto);
        void UpdateTrangThaiKpiCaNhan(UpdateTrangThaiKpiDto dto);
        Task<PageResultDto<KpiCaNhanDto>> FindPagingKpiCaNhan(FilterKpiCaNhanDto dto);
        void UpdateKetQuaThucTe(UpdateKpiThucTeKpiCaNhanListDto dto);
        List<TrangThaiResponseDto> GetListTrangThai();
    }
}
