using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Hrm
{
    public class CreateNhanSu : ICommandHandler<CreateNhanSuDto, NsNhanSuResponseDto>
    {
        public INsNhanSuService _nsNhanSuService { get; set; }

        public CreateNhanSu(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }

        public async Task<NsNhanSuResponseDto> Handle(
            CreateNhanSuDto req,
            CancellationToken cancellationToken
        )
        {
            return _nsNhanSuService.CreateNhanSu(req);
        }
    }
}
