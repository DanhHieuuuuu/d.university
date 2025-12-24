using D.DomainBase.Common;
using D.DomainBase.Dto;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc
{
    public class DmKhoaHocRequestDto : IQuery<PageResultDto<DmKhoaHocResponseDto>>
    {
        public string? Keyword { get; set; }
    }
}
