using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa;
using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa
{
    public class DmKhoaGetByIdRequestDto : IQuery<DmKhoaResponseDto>
    {
        public int Id { get; set; }
    }
}
