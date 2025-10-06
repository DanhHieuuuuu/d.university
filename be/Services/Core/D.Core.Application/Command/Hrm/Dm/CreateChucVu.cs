using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class CreateChucVu : ICommandHandler<CreateDmChucVuDto>
    {
        private readonly IDmDanhMucService _service;

        public CreateChucVu(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(CreateDmChucVuDto request, CancellationToken cancellationToken)
        {
            _service.CreateDmChucVu(request);
            return;
        }
    }
}
