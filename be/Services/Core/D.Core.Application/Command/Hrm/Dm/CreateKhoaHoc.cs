using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class CreateKhoaHoc : ICommandHandler<CreateDmKhoaHocDto>
    {
        public IDmDanhMucService _service { get; set; }

        public CreateKhoaHoc(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(CreateDmKhoaHocDto req, CancellationToken cancellationToken)
        {
            _service.CreateDmKhoaHoc(req);
            return;
        }
    }
}
