using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Hrm
{
    public class NsNhanSuRequestDto : FilterBaseDto, IQuery<PageResultDto<NsNhanSuResponseDto>>
    {
        [FromQuery(Name = "cccd")]
        public string? Cccd { get; set; }
        [FromQuery(Name = "idPhongBan")]
        public int? IdPhongBan { get; set; }
        [FromQuery(Name = "phone")]
        public string? Phone { get; set; }
    }
}
