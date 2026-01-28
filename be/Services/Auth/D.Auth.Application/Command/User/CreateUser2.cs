using D.ApplicationBase;
using D.Auth.Domain.Dtos.User;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.User
{
    public class CreateUser2 : ICommandHandler<CreateUserRequestDto2, CreateUserResponseDto2>
    {
        private readonly IUserService _userService;
        public CreateUser2(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<CreateUserResponseDto2> Handle(CreateUserRequestDto2 request, CancellationToken cancellationToken)
        {
            return await _userService.CreateUser2(request);
        }
    }
}
