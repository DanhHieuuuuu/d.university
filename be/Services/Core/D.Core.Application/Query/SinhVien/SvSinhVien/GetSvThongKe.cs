using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;

namespace D.Core.Application.Query.SinhVien.SvSinhVien
{
    public class GetSvThongKe : IQueryHandler<SvThongKeRequestDto, SvThongKeResponseDto>
    {
        private readonly ISvSinhVienService _svSinhVienService;

        public GetSvThongKe(ISvSinhVienService svSinhVienService)
        {
            _svSinhVienService = svSinhVienService;
        }

        public async Task<SvThongKeResponseDto> Handle(
            SvThongKeRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _svSinhVienService.GetThongKe();
        }
    }
}
