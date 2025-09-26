using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.NsNhanSu
{
    public class CreateHopDong : ICommandHandler<CreateHopDongDto>
    {
        public INsNhanSuService _nsNhanSuService { get; set; }

        public CreateHopDong(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }

        public async Task Handle(CreateHopDongDto req, CancellationToken cancellationToken)
        {
            await Task.Run(
                () =>
                {
                    _nsNhanSuService.CreateHopDong(req);
                },
                cancellationToken
            );
            return;
        }
    }
}
