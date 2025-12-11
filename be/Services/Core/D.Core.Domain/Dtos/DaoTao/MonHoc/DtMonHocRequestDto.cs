using D.Core.Domain.Dtos.DaoTao.MonHoc;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.MonHoc
{
    public class DtMonHocRequestDto : FilterBaseDto, IQuery<PageResultDto<DtMonHocResponseDto>>
    {
    }
}
