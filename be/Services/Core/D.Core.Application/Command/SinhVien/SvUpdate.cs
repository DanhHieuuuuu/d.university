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
    public class SvUpdate : IRequestHandler<SvSinhVienUpdateRequestDto, bool>
    {
        private readonly ISvSinhVienService _svSinhVienservice;
        public SvUpdate(ISvSinhVienService service)
        {
            _svSinhVienservice = service;
        }

        public async Task<bool> Handle(SvSinhVienUpdateRequestDto request, CancellationToken cancellationToken)
        {
            return await _svSinhVienservice.UpdateAsync(request);
        }
    }
}
