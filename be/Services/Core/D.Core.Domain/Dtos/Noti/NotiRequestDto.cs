using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Noti
{
    public class NotiRequestDto : FilterBaseDto, IQuery<PageResultDto<NotiResponseDto>>
    {
        [FromQuery(Name = "short")]
        public bool Short { get; set; }
    }
}
