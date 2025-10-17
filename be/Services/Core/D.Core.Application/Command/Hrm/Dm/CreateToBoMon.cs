using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class CreateToBoMon : ICommandHandler<CreateDmToBoMonDto>
    {
        private readonly IDmDanhMucService _service;

        public CreateToBoMon(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task Handle(CreateDmToBoMonDto request, CancellationToken cancellationToken)
        {
            _service.CreateDmToBoMon(request);
            return;
        }
    }
}
