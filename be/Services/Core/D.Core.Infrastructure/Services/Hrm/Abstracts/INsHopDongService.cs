using D.Core.Domain.Dtos.Hrm.HopDong;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface INsHopDongService
    {
        public PageResultDto<NsHopDongResponseDto> GetAllContract(NsHopDongRequestDto dto);
        public NsHopDongResponseDto GetById(int idHopDong);
        public void CreateNewContract(CreateHopDongDto dto);
        public void CreateSubContract();
    }
}
