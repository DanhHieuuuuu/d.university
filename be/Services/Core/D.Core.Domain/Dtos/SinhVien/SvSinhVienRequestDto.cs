using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class SvSinhVienRequestDto : FilterBaseDto, IQuery<PageResultDto<SvSinhVienResponseDto>>
    {
        public string? Keyword { get; set; }

        public int? KhoaHoc { get; set; }
        public int? Khoa { get; set; }
        public int? Nganh { get; set; }
    }
}
