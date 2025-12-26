using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class NsNhanSuGetAllRequestDto : FilterBaseDto, IQuery<PageResultDto<NsNhanSuGetAllResponseDto>>
    {
        [FromQuery(Name = "IdPhongBan")]
        public int? IdPhongBan { get; set; }
    }
}
