using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class CreateKhoa : ICommandHandler<CreateDmKhoaDto>
    {
        public IDmDanhMucService _service { get; set; }

        public CreateKhoa(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(CreateDmKhoaDto req, CancellationToken cancellationToken)
        {
            _service.CreateDmKhoa(req);
            return;
        }
    }
}
