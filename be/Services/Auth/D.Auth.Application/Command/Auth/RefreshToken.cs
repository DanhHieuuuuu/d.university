using D.ApplicationBase;
using D.Auth.Domain.Dtos.Login;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.Auth
{
    public class RefreshToken : ICommandHandler<RefreshTokenRequestDto, RefreshTokenResponseDto>
    {
        public INsNhanSuService _nsNhanSuService;
        public RefreshToken(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }
        public async Task<RefreshTokenResponseDto> Handle(RefreshTokenRequestDto request, CancellationToken cancellationToken)
        {
            return await _nsNhanSuService.RefreshToken(request);
        }
    }
}
