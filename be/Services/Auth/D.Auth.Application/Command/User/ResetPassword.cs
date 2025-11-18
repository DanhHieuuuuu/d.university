using D.ApplicationBase;
using D.Auth.Domain.Dtos.User.Password;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.User
{
    public class ResetPassword : ICommandHandler<ResetPasswordRequestDto, bool>
    {
        private readonly IUserService _userService;
        public ResetPassword(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(ResetPasswordRequestDto request, CancellationToken cancellationToken)
        {
            return await _userService.ResetPasswordAsync(request);
        }
    }
}
