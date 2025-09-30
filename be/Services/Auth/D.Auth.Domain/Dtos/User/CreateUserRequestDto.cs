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
        //public string? HoDem { get; set; }
        //public string? Ten { get; set; }
        //public DateTime NgaySinh { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
