using D.ApplicationBase;
using D.Auth.Domain.Dtos.UserRole;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Query.User
{
    public class GetUserRolesByUserId : IQueryHandler<GetUserRolesByUserIdRequestDto, GetUserRolesByUserIdResponseDto>
    {
        private readonly IUserService _userService;

        public GetUserRolesByUserId(IUserService userService)
        {
            _userService = userService;
        }

        public Task<GetUserRolesByUserIdResponseDto> Handle(
            GetUserRolesByUserIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _userService.GetUserRolesByUserId(request.NhanSuId);
        }
    }
}
