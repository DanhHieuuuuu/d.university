using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class DeletePhongBan : ICommandHandler<DeleteDmPhongBanDto>
    {
        private readonly IDmDanhMucService _service;

        public DeletePhongBan(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(DeleteDmPhongBanDto request, CancellationToken cancellationToken)
        {
            _service.DeleteDmPhongBan(request.Id);
            return;
        }
    }
}
