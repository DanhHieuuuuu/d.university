using D.ApplicationBase;
using D.Core.Domain.Dtos.File;
using D.Core.Infrastructure.Services.File.Abstracts;

namespace D.Core.Application.Query.File
{
    public class GetLogFilesHandler
        : IQueryHandler<GetLogFilesDto, List<LogFileInfo>>
    {
        private readonly IFileService _fileService;

        public GetLogFilesHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<List<LogFileInfo>> Handle(
            GetLogFilesDto request,
            CancellationToken cancellationToken
        )
        {
            return await _fileService.GetLogFilesAsync(request.Prefix);
        }
    }
}
