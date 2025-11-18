using D.ApplicationBase;
using D.Auth.Domain.Dtos.Login;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Query.NsNhanSu
{
    public class Login : IQueryHandler<LoginRequestDto, LoginResponseDto>
    {
        public INsNhanSuService _nsNhanSuService;
        public Login(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }
        public async Task<LoginResponseDto> Handle(LoginRequestDto request, CancellationToken cancellationToken)
        {
            return await _nsNhanSuService.Login(request);
        }
    }
}
