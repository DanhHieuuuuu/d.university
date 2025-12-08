using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.ChuyenNganh
{
    public class DtChuyenNganhGetByIdRequestDto : IQuery<DtChuyenNganhResponseDto>
    {
        public int Id { get; set; }
    }
}
