using D.DomainBase.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu
{
    public class DmChucVuGetByIdRequestDto : IQuery<DmChucVuResponseDto>
    {
        public int Id { get; set; }
    }
}
