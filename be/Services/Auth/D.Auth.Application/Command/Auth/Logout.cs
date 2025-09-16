using D.ApplicationBase;
using D.Auth.Domain.Dtos.Login;
using D.Auth.Infrastructure.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Application.Command.Auth
{
    public class Logout : ICommandHandler<LogoutRequestDto, bool>
    {
        public INsNhanSuService _nsNhanSuService;
        public Logout(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }
        public async Task<bool> Handle(LogoutRequestDto request, CancellationToken cancellationToken)
        {
            return await _nsNhanSuService.Logout(request);
        }
    }
}
