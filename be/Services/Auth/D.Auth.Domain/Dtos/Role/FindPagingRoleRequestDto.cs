using D.DomainBase.Common;
using D.DomainBase.Dto;

namespace D.Auth.Domain.Dtos.Role
{
    public class FindPagingRoleRequestDto : FilterBaseDto, IQuery<PageResultDto<RoleResponseDto>>
    {

    }
}
