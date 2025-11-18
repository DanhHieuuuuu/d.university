using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm.NsNhanSu
{
    public class FindByIdNhanSu : IQueryHandler<NsNhanSuFindByIdRequestDto, NsNhanSuFindByIdResponseDto>
    {
        private readonly INsNhanSuService _nsNhanSuService;

        public FindByIdNhanSu(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }

        public async Task<NsNhanSuFindByIdResponseDto> Handle(NsNhanSuFindByIdRequestDto request, CancellationToken cancellationToken)
        {
            return _nsNhanSuService.FindById(request.IdNhanSu);
        }
    }
}
