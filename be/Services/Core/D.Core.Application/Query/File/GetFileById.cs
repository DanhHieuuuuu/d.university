using D.ApplicationBase;
using D.Core.Domain.Dtos.File;
using D.Core.Infrastructure.Services.File.Abstracts;

namespace D.Core.Application.Query.File
{
    public class GetFileById : IQueryHandler<GetFileByIdDto, FileResponseDto>
    {
        public IFileService _fileService;

        public GetFileById(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<FileResponseDto> Handle(
            GetFileByIdDto request,
            CancellationToken cancellationToken
        )
        {
            return _fileService.GetFileById(request.Id);
        }
    }
}
