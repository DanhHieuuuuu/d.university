using D.DomainBase.Common;
using D.DomainBase.Dto;


namespace D.Core.Domain.Dtos.Kpi.KpiRole
{
    public class KpiRoleRequestDto : FilterBaseDto, IQuery<PageResultDto<KpiRoleResponseDto>> { }
}
