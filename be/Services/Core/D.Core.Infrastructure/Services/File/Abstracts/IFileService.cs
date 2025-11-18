using D.Core.Domain.Dtos.File;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.File.Abstracts
{
    public interface IFileService
    {
        PageResultDto<FileResponseDto> FindPagingFile(FileRequestDto dto);
        Task<FileResponseDto> CreateFile(CreateFileDto dto);
        Task<bool> UpdateFile(UpdateFileDto dto);
        Task<bool> DeleteFile(DeleteFileDto dto);
    }
}
