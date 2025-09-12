using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.Login
{
    public class RefreshTokenRequestDto : ICommand<RefreshTokenResponseDto>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
