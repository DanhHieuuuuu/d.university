using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;


namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class NsNhanSuByKpiRoleRequestDto : FilterBaseDto, IQuery<PageResultDto<NsNhanSuByKpiRoleResponseDto>>
    {
        [FromQuery(Name = "IdPhongBan")]
        public int? IdPhongBan { get; set; }
    }
}
