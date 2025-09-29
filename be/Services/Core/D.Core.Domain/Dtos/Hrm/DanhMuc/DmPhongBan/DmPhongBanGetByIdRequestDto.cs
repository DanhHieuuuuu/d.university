using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.DomainBase.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan
{
    public class DmPhongBanGetByIdRequestDto : IQuery<DmPhongBanResponseDto>
    {
        public int Id { get; set; }
    }
}
