using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Hrm.HopDong
{
    public class NsHopDongRequestDto : FilterBaseDto, IQuery<PageResultDto<NsHopDongResponseDto>>
    {
        [FromQuery(Name = "LoaiHopDong")]
        public int? IdLoaiHopDong { get; set; }
    }
}
