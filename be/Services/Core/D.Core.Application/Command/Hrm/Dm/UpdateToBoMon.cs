using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon;
using D.Core.Infrastructure.Services.Hrm.Abstracts;


namespace D.Core.Application.Command.Hrm.Dm
{
    public class UpdateToBoMon : ICommandHandler<UpdateDmToBoMonDto>
    {
        private readonly IDmDanhMucService _service;

        public UpdateToBoMon(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(UpdateDmToBoMonDto request, CancellationToken cancellationToken)
        {
            _service.UpdateDmToBoMon(request);
            return;
        }
    }
}
