using D.ApplicationBase;
using D.Auth.Domain.Dtos.User.Password;
using D.Auth.Infrastructure.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Application.Command.User
{
    public class ChangePassword : ICommandHandler<ChangePasswordRequestDto, bool>
    {
        private readonly IUserService _userService;
        public ChangePassword(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(ChangePasswordRequestDto request, CancellationToken cancellationToken)
        {
            return _userService.ChangePassword(request);
        }
    }
}
