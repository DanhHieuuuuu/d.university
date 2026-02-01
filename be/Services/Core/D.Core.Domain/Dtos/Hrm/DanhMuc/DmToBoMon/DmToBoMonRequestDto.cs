using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon
{
    public class DmToBoMonRequestDto : FilterBaseDto, IQuery<PageResultDto<DmToBoMonResponseDto>>
    {
        [FromQuery(Name = "idPhongBan")]
        public int? IdPhongBan { get; set; }
    }
}
