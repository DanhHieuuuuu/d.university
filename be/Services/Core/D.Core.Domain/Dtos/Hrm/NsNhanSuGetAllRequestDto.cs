using D.DomainBase.Common;
using D.DomainBase.Dto;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class NsNhanSuGetAllRequestDto : FilterBaseDto, IQuery<PageResultDto<NsNhanSuGetAllResponseDto>>
    {

    }
}
