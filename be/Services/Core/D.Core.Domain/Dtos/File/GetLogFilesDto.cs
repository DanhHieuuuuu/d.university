using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.File
{
    public class GetLogFilesDto : IQuery<List<LogFileInfo>>
    {
        public string? Prefix { get; set; }
    }

    public class LogFileInfo
    {

        public string FileName { get; set; } = string.Empty;

        public long Size { get; set; }

        public string Folder { get; set; } = string.Empty;
    }
}
