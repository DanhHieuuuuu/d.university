using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Command.Hrm.NsNhanSu
{
    public class UpdateNhanSuHandler : ICommandHandler<UpdateNhanSuDto>
    {
        private readonly INsNhanSuService _service;

        public UpdateNhanSuHandler(INsNhanSuService service)
        {
            _service = service;
        }

        public async Task Handle(UpdateNhanSuDto request, CancellationToken cancellationToken)
        {
            _service.UpdateNhanSu(request);
            return;
        }
    }
}
