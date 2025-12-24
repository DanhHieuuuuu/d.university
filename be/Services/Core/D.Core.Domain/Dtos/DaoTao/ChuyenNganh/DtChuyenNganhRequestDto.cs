using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.ChuyenNganh
{
    public class DtChuyenNganhRequestDto : FilterBaseDto, IQuery<PageResultDto<DtChuyenNganhResponseDto>>
    {
        public int? NganhId { get; set; }
    }
}
