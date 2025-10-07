using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.User
{
    public class CreateUserRequestDto : ICommand<CreateUserResponseDto>
    {
        public string MaNhanSu { get; set; }
        public string? Email2 { get; set; }
        public string? Password { get; set; }
    }
}
