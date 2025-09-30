using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm.NsNhanSu
{
    public class FindByMaNsSdt : IQueryHandler<FindByMaNsSdtDto, NsNhanSuResponseDto>
    {
        public INsNhanSuService _nsNhanSuService;

        public FindByMaNsSdt(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }

        public async Task<NsNhanSuResponseDto> Handle(
            FindByMaNsSdtDto request,
            CancellationToken cancellationToken
        )
        {
            return _nsNhanSuService.FindByMaNsSdt(request);
        }
    }
}
