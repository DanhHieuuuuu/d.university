using D.S3Bucket.Constants;
using D.S3Bucket.Dtos;
using D.S3Bucket.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace D.S3Bucket
{
    public class S3ManagerFile : IS3ManagerFile
    {
        private readonly ILogger<S3ManagerFile> _logger;
        private readonly IMinioClient _minioClient;
        private readonly D.S3Bucket.Configs.S3Config _config;

        public S3ManagerFile(
            ILogger<S3ManagerFile> logger,
            IMinioClient minioClient,
            IOptions<D.S3Bucket.Configs.S3Config> config)
        {
            _logger = logger;
            _minioClient = minioClient;
            _config = config.Value;
        }

        private string BuildFullPath(string fileName)
        {
            if (string.IsNullOrWhiteSpace(_config.FolderPrefix))
                return fileName;

            return $"{_config.FolderPrefix.TrimEnd('/')}/{fileName.TrimStart('/')}";
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                _logger.LogInformation("Testing MinIO connection...");

                var bucketName = _config.BucketName;
                var beArgs = new BucketExistsArgs().WithBucket(bucketName);
                bool found = await _minioClient.BucketExistsAsync(beArgs).ConfigureAwait(false);

                if (!found)
                {
                    _logger.LogInformation($"Bucket '{bucketName}' not found. Creating...");
                    var mbArgs = new MakeBucketArgs().WithBucket(bucketName);
                    await _minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(false);
                    _logger.LogInformation($"Bucket '{bucketName}' created successfully.");
                }
                else
                {
                    _logger.LogInformation($"Bucket '{bucketName}' exists. Connection successful.");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to MinIO");
                return false;
            }
        }

        public async Task<ResponseS3FileDto> UploadFileAsync(string? fileName, params IFormFile[] files)
        {
            _logger.LogInformation($"UploadFileAsync: Uploading {files.Length} file(s)");

            if (files == null || files.Length == 0)
            {
                throw new S3Exception(S3ErrorCode.InvalidFile, "No files provided");
            }

            var result = new ResponseS3FileDto();

            foreach (var file in files)
            {
                try
                {
                    var bucketName = _config.BucketName;
                    var objectName = BuildFullPath(fileName ?? file.FileName);
                    var fileExtension = Path.GetExtension(objectName).ToLower();

                    // Determine content type
                    string contentType = fileExtension switch
                    {
                        ".pdf" => "application/pdf",
                        ".jpg" or ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".mov" => "video/quicktime",
                        ".mp4" => "video/mp4",
                        _ => "application/octet-stream"
                    };

                    // Ensure bucket exists
                    var beArgs = new BucketExistsArgs().WithBucket(bucketName);
                    bool found = await _minioClient.BucketExistsAsync(beArgs).ConfigureAwait(false);

                    if (!found)
                    {
                        var mbArgs = new MakeBucketArgs().WithBucket(bucketName);
                        await _minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(false);
                    }

                    // Upload file
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        ms.Position = 0;

                        var putObjectArgs = new PutObjectArgs()
                            .WithBucket(bucketName)
                            .WithObject(objectName)
                            .WithStreamData(ms)
                            .WithObjectSize(ms.Length)
                            .WithContentType(contentType);

                        await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                        _logger.LogInformation($"Successfully uploaded {objectName}");
                    }

                    var fileUrl = $"{bucketName}/{objectName}";
                    result.Files.Add(new S3FileInfo
                    {
                        Url = fileUrl,
                        FileName = objectName,
                        Size = file.Length
                    });
                }
                catch (MinioException ex)
                {
                    _logger.LogError(ex, $"MinIO error uploading file {file.FileName}");
                    throw new S3Exception(S3ErrorCode.UploadError, $"Failed to upload file: {ex.Message}");
                }
            }

            return result;
        }

        public async Task<ResponseS3FileDto> UploadStreamAsync(string fileName, Stream stream, string contentType = "application/octet-stream")
        {
            _logger.LogInformation($"UploadStreamAsync: Uploading {fileName}");

            try
            {
                var bucketName = _config.BucketName;
                var objectName = BuildFullPath(fileName);

                // Ensure bucket exists
                var beArgs = new BucketExistsArgs().WithBucket(bucketName);
                bool found = await _minioClient.BucketExistsAsync(beArgs).ConfigureAwait(false);

                if (!found)
                {
                    var mbArgs = new MakeBucketArgs().WithBucket(bucketName);
                    await _minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(false);
                }

                // Upload stream
                using (var ms = new MemoryStream())
                {
                    await stream.CopyToAsync(ms);
                    ms.Position = 0;

                    var putObjectArgs = new PutObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(objectName)
                        .WithStreamData(ms)
                        .WithObjectSize(ms.Length)
                        .WithContentType(contentType);

                    await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                    _logger.LogInformation($"Successfully uploaded stream as {fileName}");
                }

                var fileUrl = $"{bucketName}/{objectName}";
                return new ResponseS3FileDto
                {
                    Files = new List<S3FileInfo>
                    {
                        new S3FileInfo
                        {
                            Url = fileUrl,
                            FileName = objectName,
                            Size = stream.Length
                        }
                    }
                };
            }
            catch (MinioException ex)
            {
                _logger.LogError(ex, $"MinIO error uploading stream {fileName}");
                throw new S3Exception(S3ErrorCode.UploadError, $"Failed to upload stream: {ex.Message}");
            }
        }

        public async Task<Stream> ReadFileAsync(string fileName)
        {
            _logger.LogInformation($"ReadFileAsync: Reading {fileName}");

            try
            {
                var bucketName = _config.BucketName;
                var objectName = BuildFullPath(fileName);
                var memoryStream = new MemoryStream();

                var getObjectArgs = new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithCallbackStream(stream => stream.CopyTo(memoryStream));

                await _minioClient.GetObjectAsync(getObjectArgs).ConfigureAwait(false);
                memoryStream.Position = 0;

                _logger.LogInformation($"Successfully read {fileName}");
                return memoryStream;
            }
            catch (MinioException ex)
            {
                _logger.LogError(ex, $"MinIO error reading file {fileName}");
                throw new S3Exception(S3ErrorCode.ReadError, $"Failed to read file: {ex.Message}");
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            _logger.LogInformation($"DeleteFileAsync: Deleting {fileName}");

            try
            {
                var bucketName = _config.BucketName;
                var objectName = BuildFullPath(fileName);

                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName);

                await _minioClient.RemoveObjectAsync(removeObjectArgs).ConfigureAwait(false);
                _logger.LogInformation($"Successfully deleted {fileName}");

                return true;
            }
            catch (MinioException ex)
            {
                _logger.LogError(ex, $"MinIO error deleting file {fileName}");
                throw new S3Exception(S3ErrorCode.DeleteError, $"Failed to delete file: {ex.Message}");
            }
        }

        public async Task<bool> FileExistsAsync(string fileName)
        {
            _logger.LogInformation($"FileExistsAsync: Checking {fileName}");

            try
            {
                var bucketName = _config.BucketName;
                var objectName = BuildFullPath(fileName);

                var statObjectArgs = new StatObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName);

                await _minioClient.StatObjectAsync(statObjectArgs).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
