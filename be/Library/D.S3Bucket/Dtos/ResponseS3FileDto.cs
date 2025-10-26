namespace D.S3Bucket.Dtos
{
    public class ResponseS3FileDto
    {
        public List<S3FileInfo> Files { get; set; } = new();
    }

    public class S3FileInfo
    {
        public string Url { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public long Size { get; set; }
    }
}
