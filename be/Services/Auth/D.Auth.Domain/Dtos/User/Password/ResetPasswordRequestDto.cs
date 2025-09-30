using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.User.Password
{
    public class ResetPasswordRequestDto : ICommand<bool>
    {
        public string MaNhanSu { get; set; }
    }
}
