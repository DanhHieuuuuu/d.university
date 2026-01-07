using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.SinhVien.Auth
{
    public class SvLoginRequestDto : IQuery<SvLoginResponseDto>
    {
        public string Mssv { get; set; }
        public string Password { get; set; }
    }
}
