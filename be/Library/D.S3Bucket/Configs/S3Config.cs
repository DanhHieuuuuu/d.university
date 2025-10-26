namespace D.S3Bucket.Configs
{
    public class S3Config
    {
        public string Endpoint { get; set; } = null!;
        public string AccessKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string BucketName { get; set; } = null!;
        public string FolderPrefix { get; set; } = string.Empty;
        public bool WithSSL { get; set; } = true;
        public string ViewMediaUrl { get; set; } = null!;
    }
}
