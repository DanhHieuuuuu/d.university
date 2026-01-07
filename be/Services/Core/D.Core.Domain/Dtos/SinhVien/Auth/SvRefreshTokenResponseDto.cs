using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.SinhVien.Auth
{
    public class SvRefreshTokenResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredRefreshToken { get; set; }
    }
}
