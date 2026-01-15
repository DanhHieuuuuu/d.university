using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiCaNhanService
    {
        Task<PageResultDto<KpiCaNhanDto>> FindPagingKpiCaNhanKeKhai(FilterKpiKeKhaiCaNhanDto dto);
        void CreateKpiCaNhan(CreateKpiCaNhanDto dto);
        void DeleteKpiCaNhan(DeleteKpiCaNhanDto dto);
        void UpdateKpiCaNhan(UpdateKpiCaNhanDto dto);
        Task UpdateTrangThaiKpiCaNhan(UpdateTrangThaiKpiDto dto);
        Task<PageResultDto<KpiCaNhanDto>> GetAllKpiCaNhan(FilterKpiCaNhanDto dto);
        void UpdateKetQuaThucTe(UpdateKpiThucTeKpiCaNhanListDto dto);
        void UpdateKetQuaCapTren(UpdateKetQuaCapTrenKpiCaNhanListDto dto);
        List<TrangThaiResponseDto> GetListTrangThai();
        int? GetKpiIsActive();
        List<GetAllNhanSuKiemNhiemResponseDto> GetAllNhanSuKiemNhiem(GetAllNhanSuKiemNhiemRequestDto dto);
        Task<List<int>> GetAllowedUserIds(int currentUserId);
        Task<object> GetKpiCaNhanContextForAi(int userId, List<int> allowedUserIds);

    }
}
