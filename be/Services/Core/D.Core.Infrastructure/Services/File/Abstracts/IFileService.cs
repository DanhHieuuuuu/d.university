using D.Core.Domain.Dtos.Delegation.Incoming;
using D.Core.Domain.Dtos.File;
using D.DomainBase.Dto;
using OpenXMLLibrary.Dtos;

namespace D.Core.Infrastructure.Services.File.Abstracts
{
    public interface IFileService
    {
        PageResultDto<FileResponseDto> FindPagingFile(FileRequestDto dto);
        FileResponseDto GetFileById(int id);
        Task<FileResponseDto> CreateFile(CreateFileDto dto);
        Task<bool> UpdateFile(UpdateFileDto dto);
        Task<bool> DeleteFile(DeleteFileDto dto);
        public ExportFileDto FillTextToDocumentTemplate(string filePath, string filename, List<InputReplaceDto> listData);
        Task<List<LogFileInfo>> GetLogFilesAsync(string? prefix);
    }
}
