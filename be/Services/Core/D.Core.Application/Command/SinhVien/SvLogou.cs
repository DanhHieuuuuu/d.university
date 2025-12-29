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
    public class SvLogoutHandler : ICommandHandler<SvLogoutRequestDto, bool>
    {
        private readonly ISvSinhVienService _service;
        public SvLogoutHandler(ISvSinhVienService service) 
        {
            _service = service; 
        }

        public async Task<bool> Handle(SvLogoutRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.Logout(request);
        }
    }
}
