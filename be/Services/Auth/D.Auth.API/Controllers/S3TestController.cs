using D.ControllerBase;
using D.S3Bucket;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace D.Auth.API.Controllers
{
    [Route("/api/s3-test")]
    [ApiController]
    public class S3TestController : APIControllerBase
    {
        private readonly IS3ManagerFile _s3ManagerFile;

        public S3TestController(IMediator mediator, IS3ManagerFile s3ManagerFile) : base(mediator)
        {
            _s3ManagerFile = s3ManagerFile;
        }

        /// <summary>
        /// Test kết nối với MinIO
        /// </summary>
        /// <returns></returns>
        [HttpGet("test-connection")]
        [AllowAnonymous]
        public async Task<ResponseAPI> TestConnection()
        {
            try
            {
                var result = await _s3ManagerFile.TestConnectionAsync();
                if (result)
                {
                    return new ResponseAPI(
                        ControllerBase.StatusCode.Success,
                        new { Message = "Kết nối MinIO thành công!", Connected = true },
                        200,
                        "Kết nối MinIO thành công!"
                    );
                }
                else
                {
                    return new ResponseAPI(
                        ControllerBase.StatusCode.Error,
                        new { Message = "Không thể kết nối đến MinIO", Connected = false },
                        500,
                        "Không thể kết nối đến MinIO"
                    );
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Upload file lên MinIO
        /// </summary>
        /// <param name="file">File cần upload</param>
        /// <param name="folderPath">Đường dẫn thư mục lưu file (vd: auth/anh_dai_dien/khoa). Nếu null sẽ lưu vào thư mục mặc định</param>
        /// <returns></returns>
        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<ResponseAPI> UploadFile(IFormFile file, [FromForm] string? folderPath)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return new ResponseAPI(
                        ControllerBase.StatusCode.Error,
                        null,
                        400,
                        "File không hợp lệ"
                    );
                }

                var result = await _s3ManagerFile.UploadFileAsync(folderPath, file);
                return new ResponseAPI(
                    ControllerBase.StatusCode.Success,
                    result,
                    200,
                    "Upload file thành công!"
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Upload nhiều file lên MinIO
        /// </summary>
        /// <param name="files">Danh sách file cần upload</param>
        /// <param name="folderPath">Đường dẫn thư mục lưu file (vd: auth/anh_dai_dien/khoa). Nếu null sẽ lưu vào thư mục mặc định</param>
        /// <returns></returns>
        [HttpPost("upload-multiple")]
        [AllowAnonymous]
        public async Task<ResponseAPI> UploadMultipleFiles([FromForm] List<IFormFile> files, [FromForm] string? folderPath)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return new ResponseAPI(
                        ControllerBase.StatusCode.Error,
                        null,
                        400,
                        "Không có file nào được chọn"
                    );
                }

                var result = await _s3ManagerFile.UploadFileAsync(folderPath, files.ToArray());
                return new ResponseAPI(
                    ControllerBase.StatusCode.Success,
                    result,
                    200,
                    $"Upload {files.Count} file thành công!"
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Kiểm tra file có tồn tại không
        /// </summary>
        /// <param name="fileName">Tên file cần kiểm tra</param>
        /// <returns></returns>
        [HttpGet("file-exists")]
        [AllowAnonymous]
        public async Task<ResponseAPI> FileExists([FromQuery] string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return new ResponseAPI(
                        ControllerBase.StatusCode.Error,
                        null,
                        400,
                        "Tên file không hợp lệ"
                    );
                }

                var exists = await _s3ManagerFile.FileExistsAsync(fileName);
                return new ResponseAPI(
                    ControllerBase.StatusCode.Success,
                    new { FileName = fileName, Exists = exists },
                    200,
                    exists ? "File tồn tại" : "File không tồn tại"
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa file từ MinIO
        /// </summary>
        /// <param name="fileName">Tên file cần xóa</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [AllowAnonymous]
        public async Task<ResponseAPI> DeleteFile([FromQuery] string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return new ResponseAPI(
                        ControllerBase.StatusCode.Error,
                        null,
                        400,
                        "Tên file không hợp lệ"
                    );
                }

                var result = await _s3ManagerFile.DeleteFileAsync(fileName);
                return new ResponseAPI(
                    ControllerBase.StatusCode.Success,
                    new { FileName = fileName, Deleted = result },
                    200,
                    "Xóa file thành công!"
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Download file từ MinIO
        /// </summary>
        /// <param name="fileName">Tên file cần download</param>
        /// <returns></returns>
        [HttpGet("download")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadFile([FromQuery] string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return BadRequest("Tên file không hợp lệ");
                }

                var stream = await _s3ManagerFile.ReadFileAsync(fileName);
                return FileByStream(stream, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách folder trong MinIO
        /// </summary>
        /// <param name="prefix">Đường dẫn prefix để lọc (vd: auth/anh_dai_dien). Nếu null sẽ lấy từ thư mục gốc</param>
        /// <returns></returns>
        [HttpGet("folders")]
        [AllowAnonymous]
        public async Task<ResponseAPI> GetFolders([FromQuery] string? prefix)
        {
            try
            {
                var folders = await _s3ManagerFile.ListFoldersAsync(prefix);
                return new ResponseAPI(
                    ControllerBase.StatusCode.Success,
                    new { Folders = folders, Count = folders.Count },
                    200,
                    $"Tìm thấy {folders.Count} folder(s)"
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
