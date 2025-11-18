using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;

namespace D.Core.Application.Command.SinhVien
{
    public class CreateSinhVien : ICommandHandler<CreateSinhVienDto, SvSinhVienResponseDto>
    {
        public ISvSinhVienService _svSinhVienService { get; set; }
        public CreateSinhVien(ISvSinhVienService svSinhVienService)
        {
            _svSinhVienService = svSinhVienService;
        }

        public async Task<SvSinhVienResponseDto> Handle(
            CreateSinhVienDto req,
            CancellationToken cancellationToken
        )
        {
            return _svSinhVienService.CreateSinhVien(req);
        }
    }
}
