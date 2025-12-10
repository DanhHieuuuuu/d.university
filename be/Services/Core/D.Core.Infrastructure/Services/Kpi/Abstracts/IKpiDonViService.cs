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
        void UpdateTrangThaiKpiDonVi(UpdateTrangThaiKpiDonViDto dto);
        //List<ViewDonViDto> GetAllDonVi();
        List<GetListYearKpiDonViDto> GetListYear();
        void GiaoKpiDonVi(GiaoKpiDonViDto dto);
        List<NhanSuDaGiaoDto> GetNhanSuByKpiDonVi(GetNhanSuFromKpiDonViDto dto);
        PageResultDto<KpiDonViDto> FindPagingKeKhai(FilterKpiDonViKeKhaiDto dto);
        void UpdateKetQuaThucTe(UpdateKpiThucTeKpiDonViListDto dto);
        List<int> GetListTrangThai();
    }
}
