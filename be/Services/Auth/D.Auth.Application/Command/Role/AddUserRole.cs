using D.ApplicationBase;
using D.Auth.Domain.Dtos.UserRole;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.Role
{
    public class AddUserRole : ICommandHandler<CreateUserRoleDto, bool>
    {
        private readonly INsNhanSuService _nsNhanSuService;

        public AddUserRole(INsNhanSuService sNhanSuService)
        {
            _nsNhanSuService = sNhanSuService;
        }

        public async Task<bool> Handle(CreateUserRoleDto request, CancellationToken cancellationToken)
        {
            return _nsNhanSuService.AddUserRole(request);
        }
    }
}
