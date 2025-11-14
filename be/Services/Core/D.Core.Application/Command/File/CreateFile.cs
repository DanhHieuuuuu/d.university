using D.ApplicationBase;
using D.Core.Domain.Dtos.File;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.File.Abstracts;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.File
{
    public class CreateFile : ICommandHandler<CreateFileDto, FileResponseDto>
    {
        public IFileService _iFileService { get; set; }
        public CreateFile(IFileService fileService)
        {
            _iFileService = fileService;
        }

        public async Task<FileResponseDto> Handle(
            CreateFileDto req,
            CancellationToken cancellationToken
        )
        {
            return await _iFileService.CreateFile(req);
        }
    }
}
