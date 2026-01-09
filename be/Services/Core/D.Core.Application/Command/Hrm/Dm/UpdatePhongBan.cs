using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class UpdatePhongBan : ICommandHandler<UpdateDmPhongBanDto>
    {
        private readonly IDmDanhMucService _service;

        public UpdatePhongBan(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(UpdateDmPhongBanDto request, CancellationToken cancellationToken)
        {
            _service.UpdateDmPhongBan(request);
            return;
        }
    }
}
