using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface INsNhanSuService
    {
        PageResultDto<NsNhanSuResponseDto> FindPagingNsNhanSu(NsNhanSuRequestDto dto);
        PageResultDto<NsNhanSuGetAllResponseDto> GetAllNhanSu(NsNhanSuGetAllRequestDto dto);
        void CreateGiaDinhNhanSu(int idNhanSu, CreateNsQuanHeGiaDinhDto dto);
        NsNhanSuResponseDto CreateNhanSu(CreateNhanSuDto dto);
        void CreateHopDong(CreateHopDongDto dto);
        NsNhanSuResponseDto FindByMaNsSdt(FindByMaNsSdtDto dto);
        NsNhanSuFindByIdResponseDto FindById(int idNhanSu);
    }
}
