using D.ApplicationBase;
using D.Auth.Domain.Dtos.User;
using D.Auth.Infrastructure.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Application.Command.User
{
    public class UpdateImageUser : ICommandHandler<UpdateImageUserDto, bool>
    {
        private readonly IUserService _userService;
        public UpdateImageUser(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(UpdateImageUserDto request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserImage(request);
        }
    }
}
