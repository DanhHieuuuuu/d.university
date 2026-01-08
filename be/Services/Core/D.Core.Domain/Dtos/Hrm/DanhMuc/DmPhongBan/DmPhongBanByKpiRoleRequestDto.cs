using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan
{
    public class DmPhongBanByKpiRoleRequestDto : FilterBaseDto, IQuery<PageResultDto<DmPhongBanByKpiRoleResponseDto>>
    {
        [FromQuery(Name = "idLoaiPhongBan")]
        public int? idLoaiPhongBan { get; set; }
    }
}
