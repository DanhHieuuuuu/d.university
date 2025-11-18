using D.Core.Domain.Dtos.File;
using D.Core.Infrastructure.Services.File.Abstracts;
using MediatR;

namespace D.Core.Application.Command.File
{
    public class DeleteFile : IRequestHandler<DeleteFileDto, bool>
    {
        private readonly IFileService _fileService;

        public DeleteFile(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<bool> Handle(DeleteFileDto request, CancellationToken cancellationToken)
        {
            return await _fileService.DeleteFile(request);
        }
    }
}

