using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class UpdateChucVu : ICommandHandler<UpdateDmChucVuDto>
    {
        private readonly IDmDanhMucService _service;

        public UpdateChucVu(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(UpdateDmChucVuDto request, CancellationToken cancellationToken)
        {
            _service.UpdateDmChucVu(request);
            return;
        }
    }
}
