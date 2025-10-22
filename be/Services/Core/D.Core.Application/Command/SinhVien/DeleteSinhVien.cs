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
    public class DeleteSinhVien : IRequestHandler<DeleteSinhVienDto, bool>
    {
        private readonly ISvSinhVienService _svSinhVienService;

        public DeleteSinhVien(ISvSinhVienService svSinhVienService)
        {
            _svSinhVienService = svSinhVienService;
        }

        public async Task<bool> Handle(DeleteSinhVienDto request, CancellationToken cancellationToken)
        {
            return await _svSinhVienService.DeleteSinhVien(request);
        }
    }
}
