using D.ApplicationBase;
using D.Core.Domain.Dtos.File;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.File.Abstracts;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.File
{
    public class FindPagingFile
        : IQueryHandler<FileRequestDto, PageResultDto<FileResponseDto>>
    {
        public IFileService _fileService;

        public FindPagingFile(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<PageResultDto<FileResponseDto>> Handle(
            FileRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _fileService.FindPagingFile(request);
        }
    }
}
