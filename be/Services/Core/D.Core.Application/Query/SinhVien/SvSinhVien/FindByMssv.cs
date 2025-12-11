using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;

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
            return await _svSinhVienService.FindByMssv(request);
        }
    }
}
