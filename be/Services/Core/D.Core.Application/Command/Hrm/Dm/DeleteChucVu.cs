using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class DeleteChucVu : ICommandHandler<DeleteDmChucVuDto>
    {
        private readonly IDmDanhMucService _service;

        public DeleteChucVu(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(DeleteDmChucVuDto request, CancellationToken cancellationToken)
        {
            _service.DeleteDmChucVu(request.Id);
            return;
        }
    }
}
