using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm.NsNhanSu
{
    public class HoSoChiTiet
        : IQueryHandler<NsNhanSuHoSoChiTietRequestDto, NsNhanSuHoSoChiTietResponseDto>
    {
        private readonly INsNhanSuService _nsNhanSuService;

        public HoSoChiTiet(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }

        public async Task<NsNhanSuHoSoChiTietResponseDto> Handle(
            NsNhanSuHoSoChiTietRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _nsNhanSuService.HoSoChiTietNhanSu(request.IdNhanSu);
        }
    }
}
