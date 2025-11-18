using D.ApplicationBase;
using D.Core.Domain.Dtos.File;
using D.Core.Infrastructure.Services.File.Abstracts;
using D.DomainBase.Dto;

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
