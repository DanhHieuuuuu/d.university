using D.DomainBase.Common;
using D.DomainBase.Dto;

namespace D.Core.Domain.Dtos.Hrm.QuyetDinh
{
    public class NsQuyetDinhRequestDto
        : FilterBaseDto,
            IQuery<PageResultDto<NsQuyetDinhResponseDto>>
    {
        public int? TrangThai { get; set; }
    }
}
