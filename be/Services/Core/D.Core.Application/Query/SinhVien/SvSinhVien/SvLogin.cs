using D.ApplicationBase;
using D.Auth.Domain.Dtos.Login;
using D.Core.Domain.Dtos.SinhVien.Auth;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.SinhVien.SvSinhVien
{
    public class SvLoginHandler : IQueryHandler<SvLoginRequestDto, SvLoginResponseDto>
    {
        private readonly ISvSinhVienService _service;
        public SvLoginHandler(ISvSinhVienService service) 
        { 
            _service = service; 
        }

        public async Task<SvLoginResponseDto> Handle(SvLoginRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.Login(request);
        }
    }
}
