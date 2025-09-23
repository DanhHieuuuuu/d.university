using D.ApplicationBase;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Domain.Dtos.User;
using D.Auth.Infrastructure.Services.Abstracts;
using D.Auth.Infrastructure.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Application.Command.User
{
    public class CreateUser : ICommandHandler<CreateUserRequestDto, CreateUserResponseDto>
    {
        private readonly IUserService _userService;
        public CreateUser(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<CreateUserResponseDto> Handle(CreateUserRequestDto request, CancellationToken cancellationToken)
        {
            return await _userService.CreateUser(request);
        }
    }
}
