using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ThongTinChiTiet;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;

namespace D.Core.Application.Query.SinhVien.SvSinhVien
{
    public class GetThongTinChiTiet : IQueryHandler<SvThongTinChiTietRequestDto, SvThongTinChiTietResponseDto>
    {
        private readonly ISvSinhVienService _svSinhVienService;

        public GetThongTinChiTiet(ISvSinhVienService svSinhVienService)
        {
            _svSinhVienService = svSinhVienService;
        }

        public async Task<SvThongTinChiTietResponseDto> Handle(
            SvThongTinChiTietRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _svSinhVienService.GetThongTinChiTiet(request);
        }
    }
}
