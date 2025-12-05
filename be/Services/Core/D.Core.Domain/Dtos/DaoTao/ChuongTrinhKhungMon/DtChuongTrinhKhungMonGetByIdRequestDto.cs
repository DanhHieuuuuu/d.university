using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon;
using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon
{
    public class DtChuongTrinhKhungMonGetByIdRequestDto : IQuery<DtChuongTrinhKhungMonResponseDto>
    {
        public int Id { get; set; }
    }
}
