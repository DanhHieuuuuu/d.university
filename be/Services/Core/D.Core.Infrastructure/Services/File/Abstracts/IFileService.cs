using D.Core.Domain.Dtos.File;
using D.Core.Domain.Dtos.SinhVien;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
