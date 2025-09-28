using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.Core.Infrastructure.Services.SinhVien.Implements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.SinhVien
{
    public class SvDelete : IRequestHandler<SvSinhVienDeleteRequestDto, bool>
    {
        private readonly ISvSinhVienService _svSinhVienservice;
        public SvDelete(ISvSinhVienService service)
        {
            _svSinhVienservice = service;
        }

        public async Task<bool> Handle(SvSinhVienDeleteRequestDto request, CancellationToken cancellationToken)
        {
            return await _svSinhVienservice.DeleteAsync(request.Id);
        }
    }
}
