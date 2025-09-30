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
    public class UpdateUser : ICommandHandler<UpdateUserRequestDto, bool>
    {
        private readonly IUserService _userService;
        public UpdateUser(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(UpdateUserRequestDto request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUser(request);
        }
    }
}
