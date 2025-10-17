using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class DeleteToBoMon : ICommandHandler<DeleteDmToBoMonDto>
    {
        private readonly IDmDanhMucService _service;
        public DeleteToBoMon(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }
        public async Task Handle(DeleteDmToBoMonDto request, CancellationToken cancellationToken)
        {
            _service.DeleteDmToBoMon(request.Id);
            return;
        }
    }
}
