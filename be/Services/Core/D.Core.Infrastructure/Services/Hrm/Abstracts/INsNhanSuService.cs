using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface INsNhanSuService
    {
        public PageResultDto<NsNhanSuResponseDto> FindPagingNsNhanSu(NsNhanSuRequestDto dto);
        public PageResultDto<NsNhanSuGetAllResponseDto> GetAllNhanSu(NsNhanSuGetAllRequestDto dto);
        public PageResultDto<NsNhanSuByKpiRoleResponseDto> GetAllNhanSuByKpiRole(NsNhanSuByKpiRoleRequestDto dto);
        public NsNhanSuResponseDto CreateNhanSu(CreateNhanSuDto dto);
        public void UpdateThongTinCongViec(UpdateNhanSuCongViecDto dto);
        public void UpdateNhanSu(UpdateNhanSuDto dto);
        public NsNhanSuResponseDto FindByMaNsSdt(FindByMaNsSdtDto dto);
        public NsNhanSuFindByIdResponseDto FindById(int idNhanSu);
        public NsNhanSuHoSoChiTietResponseDto HoSoChiTietNhanSu(int idNhanSu);

    }
}
