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
        /// <param name="folderPath">Đường dẫn thư mục lưu file (vd: auth/anh_dai_dien/khoa). Nếu null sẽ lưu vào thư mục mặc định</param>
        /// <param name="files">Danh sách file cần upload</param>
        Task<ResponseS3FileDto> UploadFileAsync(string? folderPath, params IFormFile[] files);

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

        /// <summary>
        /// Lấy danh sách folder trong MinIO
        /// </summary>
        /// <param name="prefix">Đường dẫn prefix để lọc (vd: auth/anh_dai_dien/). Nếu null sẽ lấy từ thư mục gốc</param>
        Task<List<string>> ListFoldersAsync(string? prefix = null);
    }
}
