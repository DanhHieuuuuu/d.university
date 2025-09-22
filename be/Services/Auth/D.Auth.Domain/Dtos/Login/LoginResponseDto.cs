using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredRefreshToken { get; set; }
        public NsNhanSuResponseDto User { get; set; }
    }
}
