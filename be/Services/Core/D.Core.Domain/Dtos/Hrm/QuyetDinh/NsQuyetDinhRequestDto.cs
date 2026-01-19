using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Hrm.QuyetDinh
{
    public class NsQuyetDinhRequestDto
        : FilterBaseDto,
            IQuery<PageResultDto<NsQuyetDinhResponseDto>>
    {
        [FromQuery(Name = "status")]
        public int? TrangThai { get; set; }

        [FromQuery(Name = "loaiQuyetDinh")]
        public int? LoaiQuyetDinh { get; set; }
    }
}
