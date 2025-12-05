using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.Khoa
{
    public class DtKhoaRequestDto : FilterBaseDto, IQuery<PageResultDto<DtKhoaResponseDto>>
    {
    }
}
