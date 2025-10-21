using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class SvSinhVienGetAllRequestDto : FilterBaseDto, IQuery<PageResultDto<SvSinhVienGetAllResponseDto>>
    {
        [FromQuery(Name = "mssv")]
        public string? Mssv { get; set; }
    }
}
