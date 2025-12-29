using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.SinhVien.Auth
{
    public class SvRefreshTokenRequestDto : ICommand<SvRefreshTokenResponseDto>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
