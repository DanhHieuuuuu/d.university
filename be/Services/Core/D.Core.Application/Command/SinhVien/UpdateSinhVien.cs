using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using MediatR;

namespace D.Core.Application.Command.SinhVien
{
    public class UpdateSinhVien : ICommandHandler<UpdateSinhVienDto, bool>
    {
        private readonly ISvSinhVienService _svSinhVienService;

        public UpdateSinhVien(ISvSinhVienService svSinhVienService)
        {
            _svSinhVienService = svSinhVienService;
        }

        public async Task<bool> Handle(UpdateSinhVienDto request, CancellationToken cancellationToken)
        {
            return await _svSinhVienService.UpdateSinhVien(request);
        }
    }
}
