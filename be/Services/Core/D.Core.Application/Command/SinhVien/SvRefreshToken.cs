using D.ApplicationBase;
using D.Auth.Domain.Dtos.Login;
using D.Core.Domain.Dtos.SinhVien.Auth;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.SinhVien
{
    public class SvRefreshTokenHandler : ICommandHandler<SvRefreshTokenRequestDto, SvRefreshTokenResponseDto>
    {
        private readonly ISvSinhVienService _service;
        public SvRefreshTokenHandler(ISvSinhVienService service) 
        { 
            _service = service; 
        }

        public async Task<SvRefreshTokenResponseDto> Handle(SvRefreshTokenRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.RefreshToken(request);
        }
    }
}
