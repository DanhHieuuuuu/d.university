using D.Core.Domain.Dtos.File;
using D.Core.Infrastructure.Services.File.Abstracts;
using MediatR;

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