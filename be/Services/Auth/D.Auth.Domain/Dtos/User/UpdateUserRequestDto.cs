using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.User
{
    public class UpdateUserRequestDto : ICommand<bool>
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
    }
}
