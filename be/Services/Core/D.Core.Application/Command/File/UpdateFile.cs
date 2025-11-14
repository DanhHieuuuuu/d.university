using D.Core.Domain.Dtos.File;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.File.Abstracts;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.File
{
    public class UpdateFile : IRequestHandler<UpdateFileDto, bool>
    {
        private readonly IFileService _fileService;

        public UpdateFile(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<bool> Handle(UpdateFileDto request, CancellationToken cancellationToken)
        {
            return await _fileService.UpdateFile(request);
        }
    }
}