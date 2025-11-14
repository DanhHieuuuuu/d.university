using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.File
{
    public class FileRequestDto : FilterBaseDto, IQuery<PageResultDto<FileResponseDto>>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
