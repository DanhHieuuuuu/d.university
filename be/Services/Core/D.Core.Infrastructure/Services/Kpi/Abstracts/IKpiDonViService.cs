using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiDonViService
    {
        PageResultDto<KpiDonViDto> GetAllKpiDonVi(FilterKpiDonViDto dto);
        void CreateKpiDonVi(CreateKpiDonViDto dto);
        void UpdateKpiDonVi(UpdateKpiDonViDto dto);
        void DeleteKpiDonVi(DeleteKpiDonViDto dto);
        Task UpdateTrangThaiKpiDonVi(UpdateTrangThaiKpiDonViDto dto);
        List<GetListYearKpiDonViDto> GetListYear();
        Task GiaoKpiDonVi(GiaoKpiDonViDto dto);
        List<NhanSuDaGiaoDto> GetNhanSuByKpiDonVi(GetNhanSuFromKpiDonViDto dto);
        PageResultDto<KpiDonViDto> FindPagingKeKhai(FilterKpiDonViKeKhaiDto dto);
        void UpdateKetQuaThucTe(UpdateKpiThucTeKpiDonViListDto dto);
        void UpdateKetQuaCapTren(UpdateKetQuaCapTrenKpiDonViListDto dto);
        List<TrangThaiKpiDonViResponseDto> GetListTrangThai();
        Task<GetTrangThaiKpiTruongByKpiDonViResponseDto> GetTrangThaiKpiTruongByKpiDonViAsync(GetTrangThaiKpiTruongByKpiDonViRequestDto dto);
        int? GetKpiIsActive();
        Task<object> GetKpiDonViContextForAi(List<int>? allowedDonViIds);

    }
}
