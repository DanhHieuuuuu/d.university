using D.ApplicationBase;
using D.Auth.Domain.Dtos.User;
using D.Auth.Domain.Dtos.User.Password;
using D.Auth.Infrastructure.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Application.Command.User
{
    public class ChangeStatusUser : ICommandHandler<ChangeStatusUserDto, bool>
    {
        private readonly IUserService _userService;
        public ChangeStatusUser(IUserService userService)
        {
            _userService = userService;
        }
        public Task<bool> Handle(ChangeStatusUserDto request, CancellationToken cancellationToken)
        {
            return _userService.ChangeStatusUser(request.NhanSuId);
        }
    }
}
