using D.DomainBase.Common;
using D.DomainBase.Dto;

namespace D.Auth.Domain.Dtos
{
    public class NsNhanSuRequestDto : FilterBaseDto, IQuery<PageResultDto<NsNhanSuResponseDto>>
    {

    }
}
