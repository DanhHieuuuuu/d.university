using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class NsNhanSuGetAllRequestDto : FilterBaseDto, IQuery<PageResultDto<NsNhanSuGetAllResponseDto>>
    {
        [FromQuery(Name = "keyword")]
        public string? Keyword { get; set; }
    }
}
