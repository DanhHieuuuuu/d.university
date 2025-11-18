using D.ApplicationBase;
using D.Auth.Domain.Dtos.User;
using D.Auth.Infrastructure.Services.Abstracts;

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
