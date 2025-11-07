namespace D.Notification.ApplicationService.Configs
{
    /// <summary>
    /// Cấu hình cho Smtp Gmail
    /// https://security.google.com/settings/security/apppasswords
    /// </summary>
    public class SmtpConfig
    {
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public int Timeout { get; set; }
    }
}
