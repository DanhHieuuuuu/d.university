using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.File
{
    public class FileRequestDto : FilterBaseDto, IQuery<PageResultDto<FileResponseDto>>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
