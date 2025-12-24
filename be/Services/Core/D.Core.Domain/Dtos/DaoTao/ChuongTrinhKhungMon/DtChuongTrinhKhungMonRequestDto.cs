using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon
{
    public class DtChuongTrinhKhungMonRequestDto : FilterBaseDto, IQuery<PageResultDto<DtChuongTrinhKhungMonResponseDto>>
    {
        public int? ChuongTrinhKhungId { get; set; }
        public int? HocKy { get; set; }
    }
}
