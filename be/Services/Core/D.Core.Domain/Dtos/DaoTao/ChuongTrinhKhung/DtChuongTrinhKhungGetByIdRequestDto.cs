using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung;
using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung
{
    public class DtChuongTrinhKhungGetByIdRequestDto : IQuery<DtChuongTrinhKhungResponseDto>
    {
        public int Id { get; set; }
    }
}