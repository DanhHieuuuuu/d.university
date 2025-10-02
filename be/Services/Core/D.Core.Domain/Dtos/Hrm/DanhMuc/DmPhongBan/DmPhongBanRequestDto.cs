using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan
{
    public class DmPhongBanRequestDto : FilterBaseDto, IQuery<PageResultDto<DmPhongBanResponseDto>>
    {
        [FromQuery(Name = "idLoaiPhongBan")]
        public int? idLoaiPhongBan { get; set; }
    }
}
