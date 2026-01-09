using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.HopDong;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.HopDong
{
    public class CreateHopDong : ICommandHandler<CreateHopDongDto>
    {
        public INsContractService _nsContractService { get; set; }

        public CreateHopDong(INsContractService nsContractService)
        {
            _nsContractService = nsContractService;
        }

        public async Task Handle(CreateHopDongDto req, CancellationToken cancellationToken)
        {
            _nsContractService.CreateNewContract(req);
            return;
        }
    }
}
