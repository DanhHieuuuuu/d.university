using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.SinhVien.SvSinhVien
{
    public class FindByMssv : IQueryHandler<FindByMssvDto, SvSinhVienResponseDto>
    {
        public ISvSinhVienService _svSinhVienService;

        public FindByMssv(ISvSinhVienService nsSinhVienService)
        {
            _svSinhVienService = nsSinhVienService;
        }

        public async Task<SvSinhVienResponseDto> Handle(
            FindByMssvDto request,
            CancellationToken cancellationToken
        )
        {
            return _svSinhVienService.FindByMssv(request);
        }
    }
}
