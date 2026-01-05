using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;


namespace D.Core.Domain.Dtos.Kpi.KpiRole
{
    public class KpiRoleRequestDto : FilterBaseDto, IQuery<PageResultDto<KpiRoleResponseDto>> 
    {
        [FromQuery(Name = "chucVu")]
        public string? Role { get; set; }
    }
}
