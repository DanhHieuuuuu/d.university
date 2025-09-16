using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.Login
{
    public class LoginRequestDto : IQuery<LoginResponseDto>
    {
        public string MaNhanSu { get; set; }
        public string Password { get; set; }
    }
}
