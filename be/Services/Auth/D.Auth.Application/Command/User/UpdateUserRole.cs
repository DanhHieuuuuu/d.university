using D.ApplicationBase;
using D.Auth.Domain.Dtos.UserRole;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.User
{
    public class UpdateUserRole : ICommandHandler<UpdateUserRoleDto, bool>
    {
        private readonly IUserService _userService;
        public UpdateUserRole(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(UpdateUserRoleDto request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserRoles(request);
        }
    }
}
