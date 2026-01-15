using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiTruongService
    {
        PageResultDto<KpiTruongDto> GetAllKpiTruong(FilterKpiTruongDto dto);
        List<GetListKpiTruongResponseDto> GetListKpiTruong();
        Task CreateKpiTruong(CreateKpiTruongDto dto);
        Task UpdateKpiTruong(UpdateKpiTruongDto dto);
        Task GiaoKpiHieuTruong(GiaoKpiHieuTruongDto dto);
        Task DeleteKpi(DeleteKpiTruongDto dto);
        List<GetListYearKpiTruongDto> GetListYear();
        List<TrangThaiKpiTruongResponseDto> GetListTrangThai();
        Task UpdateKetQuaThucTe(UpdateKpiThucTeKpiTruongListDto dto);
        Task UpdateTrangThaiKpiTruong(UpdateTrangThaiKpiTruongDto dto);
        void UpdateKetQuaCapTren(UpdateKetQuaCapTrenKpiTruongListDto dto);
        Task<List<object>> GetKpiTruongContextForAi();
    }
}

