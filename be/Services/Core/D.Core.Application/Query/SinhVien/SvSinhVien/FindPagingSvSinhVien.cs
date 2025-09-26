using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.SinhVien.SvSinhVien
{
    public class FindPagingSvSinhVien
        : IQueryHandler<SvSinhVienRequestDto, PageResultDto<SvSinhVienResponseDto>>
    {
        public ISvSinhVienService _svSinhVienService;

        public FindPagingSvSinhVien(ISvSinhVienService svSinhVienService)
        {
            _svSinhVienService = svSinhVienService;
        }

        public async Task<PageResultDto<SvSinhVienResponseDto>> Handle(
            SvSinhVienRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _svSinhVienService.FindPagingSvSinhVien(request);
        }
    }
}
