using AutoMapper;
using AutoMapper.QueryableExtensions;
using D.Core.Domain.Dtos.File;
using D.Core.Domain.Entities.File;
using D.Core.Infrastructure.Services.File.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.S3Bucket;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.File.Implements
{
    public class FileService : ServiceBase, IFileService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IS3ManagerFile _s3ManagerFile;

        public FileService(
            ILogger<FileService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IS3ManagerFile s3ManagerFile
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _s3ManagerFile = s3ManagerFile;
        }

        public PageResultDto<FileResponseDto> FindPagingFile(FileRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindPagingFile)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iFileRepository.TableNoTracking.Where(x =>
                (string.IsNullOrEmpty(dto.Name) || x.Name.Contains(dto.Name)) && x.Deleted != true
            );

            var totalCount = query.Count();

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<FileResponseDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new PageResultDto<FileResponseDto> { Items = items, TotalItem = totalCount };
        }

        public async Task<FileResponseDto> CreateFile(CreateFileDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateFile)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            if (dto.File == null)
                throw new Exception("File không được để trống.");

            var exist = _unitOfWork.iFileRepository.IsNameExist(dto.Name!);

            if (exist)
                throw new Exception("Tên file đã tồn tại trong hệ thống.");

            await using var transaction = await _unitOfWork.Database.BeginTransactionAsync();

            try
            {
                var newFile = _mapper.Map<FileManagement>(dto);

                _unitOfWork.iFileRepository.Add(newFile);
                await _unitOfWork.SaveChangesAsync();

                var fileExtension = Path.GetExtension(dto.File.FileName);
                var uuid = Guid.NewGuid().ToString();
                var fileName = $"{uuid}{fileExtension}";
                var uploadResult = await _s3ManagerFile.UploadFileAsync(fileName, dto.File);

                if (uploadResult?.Files?.FirstOrDefault() == null)
                    throw new Exception("Upload file lên MinIO thất bại.");

                newFile.Link = fileName;
                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();

                return _mapper.Map<FileResponseDto>(newFile);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Lỗi khi tạo file");
                throw;
            }
        }

        public async Task<bool> UpdateFile(UpdateFileDto dto)
        {
            var file = _unitOfWork.iFileRepository.GetById(dto.Id!);
            if (file == null)
                throw new Exception("File không tồn tại trong hệ thống.");

            var NameExist = _unitOfWork.iFileRepository.IsNameExist(dto.Name!);
            if (NameExist)
                throw new Exception("Tên File đã tồn tại trong hệ thống.");

            await using var transaction = await _unitOfWork.Database.BeginTransactionAsync();
            var uuid = Guid.NewGuid().ToString();

            try
            {
                if (!string.IsNullOrWhiteSpace(dto.Name))
                    file.Name = dto.Name;

                if (!string.IsNullOrWhiteSpace(dto.Description))
                    file.Description = dto.Description;

                if (!string.IsNullOrWhiteSpace(dto.ApplicationField))
                    file.ApplicationField = dto.ApplicationField;

                if (dto.File != null)
                {
                    var fileExtension = Path.GetExtension(dto.File.FileName);
                    var fileName = $"{uuid}{fileExtension}";
                    var uploadResult = await _s3ManagerFile.UploadFileAsync(fileName, dto.File);

                    if (uploadResult?.Files?.FirstOrDefault() == null)
                        throw new Exception("Upload file lên MinIO thất bại.");

                    file.Link = fileName;
                }

                _unitOfWork.iFileRepository.Update(file);
                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Lỗi khi cập nhật file");
                throw;
            }
        }



        public async Task<bool> DeleteFile(DeleteFileDto dto)
        {
            var file = _unitOfWork.iFileRepository.GetById(dto.Id!);
            if (file == null)
                throw new Exception("File không tồn tại trong hệ thống.");
            else
            {
                file.Deleted = true;
                file.DeletedDate = DateTime.Now;
                _unitOfWork.iFileRepository.Update(file);
                await _unitOfWork.iFileRepository.SaveChangeAsync();
                return true;
            }
        }
    }
}
