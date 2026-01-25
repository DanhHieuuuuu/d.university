using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm
{
    public class ThongKeNhanSuTheoPhongBanHandler
        : IQueryHandler<
            ThongKeNhanSuTheoPhongBanRequestDto,
            List<ThongKeNhanSuTheoPhongBanResponseDto>
        >
    {
        private readonly INsNhanSuService _service;

        public ThongKeNhanSuTheoPhongBanHandler(INsNhanSuService service)
        {
            _service = service;
        }

        public async Task<List<ThongKeNhanSuTheoPhongBanResponseDto>> Handle(
            ThongKeNhanSuTheoPhongBanRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.ThongKeNhanSuTheoPhongBan(request);
        }
    }
}
