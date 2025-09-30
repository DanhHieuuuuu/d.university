using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.User.Password
{
    public class ChangePasswordRequestDto : ICommand<bool>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
