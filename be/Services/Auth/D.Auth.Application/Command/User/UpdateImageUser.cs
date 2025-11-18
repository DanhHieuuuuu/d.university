using D.ApplicationBase;
using D.Auth.Domain.Dtos.User;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.User
{
    public class UpdateImageUser : ICommandHandler<UpdateImageUserDto, Stream>
    {
        private readonly IUserService _userService;
        public UpdateImageUser(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Stream> Handle(UpdateImageUserDto request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserImage(request);
        }
    }
}
