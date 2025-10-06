using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class CreatePhongBan : ICommandHandler<CreateDmPhongBanDto>
    {
        public IDmDanhMucService _service { get; set; }

        public CreatePhongBan(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(CreateDmPhongBanDto req, CancellationToken cancellationToken)
        {
            _service.CreateDmPhongBan(req);
            return;
        }
    }
}
