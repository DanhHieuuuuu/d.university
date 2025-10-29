using D.S3Bucket.Dtos;
using Microsoft.AspNetCore.Http;

namespace D.S3Bucket
{
    /// <summary>
    /// Quản lý file với MinIO S3
    /// </summary>
    public interface IS3ManagerFile
    {
        /// <summary>
        /// Upload file lên MinIO
        /// </summary>
        Task<ResponseS3FileDto> UploadFileAsync(string? fileName, params IFormFile[] files);

        /// <summary>
        /// Upload stream lên MinIO
        /// </summary>
        Task<ResponseS3FileDto> UploadStreamAsync(string fileName, Stream stream, string contentType = "application/octet-stream");

        /// <summary>
        /// Đọc file từ MinIO
        /// </summary>
        Task<Stream> ReadFileAsync(string fileName);

        /// <summary>
        /// Xóa file từ MinIO
        /// </summary>
        Task<bool> DeleteFileAsync(string fileName);

        /// <summary>
        /// Kiểm tra file có tồn tại không
        /// </summary>
        Task<bool> FileExistsAsync(string fileName);

        /// <summary>
        /// Test kết nối với MinIO
        /// </summary>
        Task<bool> TestConnectionAsync();
    }
}
