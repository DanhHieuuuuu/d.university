using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.SinhVien
{
    public class SvCreate : IRequestHandler<SvSinhVienCreateRequestDto, int>
    {
        private readonly ISvSinhVienService _svSinhVienservice;
        public SvCreate(ISvSinhVienService service)
        {
            _svSinhVienservice = service;
        }

        public async Task<int> Handle(SvSinhVienCreateRequestDto request, CancellationToken cancellationToken)
        {
            return await _svSinhVienservice.CreateAsync(request);
        }
    }
}
