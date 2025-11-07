namespace D.Notification.Domain.Exceptions
{
    public static class NotificationErrorCode
    {
        #region Lỗi chung
        public const int NotiBaseError = 50001;

        #endregion

        #region SMTP
        public const int SmtpError = 50010;
        public const int SmtpAuthError = 50011;
        public const int SmtpConfigError = 50012;
        public const int SmtpTimeoutError = 50013;
        public const int EmailAddressInvalid = 50014;
        public const int SmtpSendError = 50015;

        #endregion

        #region SMS
        public const int SmsError = 50020;

        #endregion

        #region Push (Firebase, APN, ...)
        public const int FirebaseError = 50030;

        #endregion

        #region Realtime (WebSocket, SignalR, ...)
        public const int WebSocketError = 50040;

        #endregion


        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { NotiBaseError, "Lỗi hệ thống, không gửi được thông báo" },

            { SmtpError, "Lỗi khi kết nối tới SMTP" },
            { SmtpAuthError, "Lỗi xác thực tài khoản SMTP" },
            { SmtpConfigError, "Lỗi cấu hình SMTP (Host/Port/SSL không đúng)" },
            { SmtpTimeoutError, "Quá thời gian chờ khi gửi thư" },
            { EmailAddressInvalid, "Địa chỉ email người nhận không hợp lệ" },
            { SmtpSendError, "Server từ chối gửi email tới người nhận" },
        };
    }
}
