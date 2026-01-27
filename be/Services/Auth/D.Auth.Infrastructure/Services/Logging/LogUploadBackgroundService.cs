using D.S3Bucket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace D.Auth.Infrastructure.Services.Logging
{
    public class LogUploadBackgroundService : BackgroundService
    {
        private readonly ILogger<LogUploadBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        private readonly TimeSpan _uploadInterval;
        private readonly string _logDirectory;
        private readonly string _s3FolderPrefix;
        private readonly bool _deleteAfterUpload;

        public LogUploadBackgroundService(
            ILogger<LogUploadBackgroundService> logger,
            IServiceScopeFactory serviceScopeFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;

            // Read configuration
            var intervalHours = _configuration.GetValue<int>("LogUpload:IntervalHours", 24);
            _uploadInterval = TimeSpan.FromHours(intervalHours);
            _deleteAfterUpload = _configuration.GetValue<bool>("LogUpload:DeleteAfterUpload", false);
            _s3FolderPrefix = _configuration.GetValue<string>("LogUpload:S3FolderPrefix", "logs/auth-api") ?? "logs/auth-api";
            _logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Log Upload Background Service started. Upload interval: {Interval} hours", _uploadInterval.TotalHours);

            // Upload immediately on startup
            try
            {
                await UploadOldLogFilesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during initial log upload");
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(_uploadInterval, stoppingToken);
                    await UploadOldLogFilesAsync();
                }
                catch (TaskCanceledException)
                {
                    // Expected when stopping
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading log files to S3");
                }
            }

            _logger.LogInformation("Log Upload Background Service stopped.");
        }

        private async Task UploadOldLogFilesAsync()
        {
            if (!Directory.Exists(_logDirectory))
            {
                _logger.LogWarning("Log directory does not exist: {LogDirectory}", _logDirectory);
                return;
            }

            var logFiles = Directory.GetFiles(_logDirectory, "*.txt");
            var yesterday = DateTime.Now.AddDays(-1).Date;
            var uploadedCount = 0;

            _logger.LogInformation("Scanning {Count} log files for upload", logFiles.Length);

            // Create a scope to resolve scoped services
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var s3Service = scope.ServiceProvider.GetRequiredService<IS3ManagerFile>();

                foreach (var filePath in logFiles)
                {
                    try
                    {
                        var fileInfo = new FileInfo(filePath);

                        // Only upload files older than 1 day except current log file)
                        if (fileInfo.LastWriteTime.Date >= yesterday)
                        {
                            _logger.LogDebug("Skipping current log file: {FileName}", fileInfo.Name);
                            continue;
                        }

                        var fileName = Path.GetFileName(filePath);
                        var s3Key = $"{_s3FolderPrefix}/{fileName}";

                        _logger.LogInformation("Uploading log file to S3: {FileName} -> {S3Key}", fileName, s3Key);

                        using (var fileStream = File.OpenRead(filePath))
                        {
                            await s3Service.UploadStreamAsync(s3Key, fileStream, "text/plain");
                        }

                        _logger.LogInformation("Successfully uploaded {FileName} to S3", fileName);
                        uploadedCount++;

                        // Delete local file if configured
                        if (_deleteAfterUpload)
                        {
                            File.Delete(filePath);
                            _logger.LogInformation("Deleted local log file: {FileName}", fileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to upload log file: {FilePath}", filePath);
                    }
                }
            }

            if (uploadedCount > 0)
            {
                _logger.LogInformation("Log upload completed. Uploaded {Count} files to S3", uploadedCount);
            }
            else
            {
                _logger.LogInformation("No old log files to upload");
            }
        }
    }
}
