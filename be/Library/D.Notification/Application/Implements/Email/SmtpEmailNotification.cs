using D.Notification.Application.Abstracts.Email;
using D.Notification.Application.Abstracts.Logging;
using D.Notification.ApplicationService.Configs;
using D.Notification.Domain.Entities;
using D.Notification.Domain.Exceptions;
using D.Notification.Domain.Repositories;
using D.Notification.Dtos;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net; 
using System.Text.Json;

namespace D.Notification.ApplicationService.Implements.Email
{
    public class SmtpEmailNotification : IEmailNotification
    {
        private readonly SmtpConfig _config;
        private readonly ILogger<SmtpEmailNotification> _logger;
        private readonly INotificationLogService _notificationLogService;
        private readonly INotificationRepository _notificationRepository;

        public SmtpEmailNotification(
            IOptions<SmtpConfig> config,
            ILogger<SmtpEmailNotification> logger,
            INotificationLogService logService,
            INotificationRepository notificationRepository
        )
        {
            _config = config.Value;
            _logger = logger;
            _notificationLogService = logService;
            _notificationRepository = notificationRepository;
        }

        public async Task SendEmailAsync(NotificationMessage dto)
        {
            _logger.LogInformation(
                "SendEmailAsync (Force IPv4) called. Receiver: {Email}, Title: {Title}",
                dto.Receiver.Email,
                dto.Title
            );

            if (string.IsNullOrWhiteSpace(dto.Receiver.Email))
                throw new ArgumentException("Receiver email is empty");

            // --- 1. TẠO EMAIL ---
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config.DisplayName, _config.SenderEmail));
            message.To.Add(new MailboxAddress("", dto.Receiver.Email));
            message.Subject = dto.Title;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = dto.Content,
                TextBody = dto.AltContent
            };
            message.Body = bodyBuilder.ToMessageBody();

            // --- 2. LƯU LOG VÀO DB ---
            var notificationEntity = new NotiNotificationDetail
            {
                Title = dto.Title,
                Content = dto.AltContent,
                IsRead = false,
                Receiver = dto.Receiver.Email,
                ReceiverId = dto.Receiver.UserId ?? 0,
            };

            await _notificationRepository.AddAsync(notificationEntity);
            await _notificationRepository.SaveChangesAsync();

            // --- 3. GỬI MAIL (FORCE IPv4) ---
            using var client = new SmtpClient();

            try
            {
                // [FIX 1] Bỏ qua lỗi SSL vì khi kết nối bằng IP, chứng chỉ sẽ không khớp domain
                client.CheckCertificateRevocation = false;
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                // [FIX 2] Tự tìm IP IPv4 để tránh bị Render ép dùng IPv6 (Nguyên nhân gây Timeout)
                string hostToConnect = _config.Host;
                try
                {
                    // Lấy tất cả địa chỉ IP của smtp.gmail.com
                    var ipAddresses = await Dns.GetHostAddressesAsync(_config.Host);
                    // Chỉ lấy địa chỉ IPv4 (InterNetwork)
                    var ipv4 = ipAddresses.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                    if (ipv4 != null)
                    {
                        hostToConnect = ipv4.ToString();
                        _logger.LogInformation("Resolved {Host} to IPv4: {IP}", _config.Host, hostToConnect);
                    }
                }
                catch (Exception dnsEx)
                {
                    _logger.LogWarning("DNS lookup failed, using original host. Error: {Error}", dnsEx.Message);
                }

                // Kết nối vào IP IPv4 vừa tìm được
                // Dùng Auto để nó tự thích nghi với cả Port 587 và 465
                await client.ConnectAsync(hostToConnect, _config.Port, SecureSocketOptions.Auto);

                await client.AuthenticateAsync("a0b9bb001@smtp-brevo.com", _config.SenderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation("Email sent successfully to {Email}", dto.Receiver.Email);
                await _notificationLogService.LogSuccessAsync(notificationEntity.Id, dto.Receiver.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MailKit Error while sending to {Email}. Host: {Host}, Port: {Port}", dto.Receiver.Email, _config.Host, _config.Port);

                var errorCode = NotificationErrorCode.SmtpError;

                if (ex.Message.Contains("Authentication")) errorCode = NotificationErrorCode.SmtpAuthError;
                else if (ex is ServiceNotConnectedException || ex.Message.Contains("Connect") || ex is System.TimeoutException) errorCode = NotificationErrorCode.SmtpTimeoutError;
                else if (ex is CommandException) errorCode = NotificationErrorCode.SmtpSendError;

                await _notificationLogService.LogFailureAsync(notificationEntity.Id, dto.Receiver.Email, errorCode);
                throw new NotificationException(errorCode, ex.Message);
            }
        }
    }
}

//COde cũ



//using D.Notification.Application.Abstracts.Email;
//using D.Notification.Application.Abstracts.Logging;
//using D.Notification.ApplicationService.Configs;
//using D.Notification.Domain.Entities;
//using D.Notification.Domain.Exceptions;
//using D.Notification.Domain.Repositories;
//using D.Notification.Dtos;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System.Net;
//using System.Net.Mail;
//using System.Security.Authentication;
//using System.Text.Json;

//namespace D.Notification.ApplicationService.Implements.Email
//{
//    public class SmtpEmailNotification : IEmailNotification
//    {
//        private readonly SmtpConfig _config;
//        private readonly ILogger<SmtpEmailNotification> _logger;
//        private readonly INotificationLogService _notificationLogService;
//        private readonly INotificationRepository _notificationRepository;

//        public SmtpEmailNotification(
//            IOptions<SmtpConfig> config,
//            ILogger<SmtpEmailNotification> logger,
//            INotificationLogService logService,
//            INotificationRepository notificationRepository
//        )
//        {
//            _config = config.Value;
//            _logger = logger;
//            _notificationLogService = logService;
//            _notificationRepository = notificationRepository;
//        }

//        public async Task SendEmailAsync(NotificationMessage dto)
//        {
//            _logger.LogInformation(
//                "SendEmailAsync called. Receiver: {Email}, Title: {Title}",
//                dto.Receiver.Email,
//                dto.Title
//            );

//            if (string.IsNullOrWhiteSpace(dto.Receiver.Email))
//                throw new ArgumentException("Receiver email is empty");

//            using var smtpClient = new SmtpClient(_config.Host, _config.Port)
//            {
//                EnableSsl = _config.EnableSSL,
//                DeliveryMethod = SmtpDeliveryMethod.Network,
//                UseDefaultCredentials = false,
//                Credentials = new NetworkCredential(_config.SenderEmail, _config.SenderPassword),
//                Timeout = _config.Timeout,
//            };

//            var message = new MailMessage
//            {
//                From = new MailAddress(_config.SenderEmail, _config.DisplayName),
//                Subject = dto.Title,
//                Body = dto.Content,
//                IsBodyHtml = true,
//                BodyEncoding = System.Text.Encoding.UTF8,
//                SubjectEncoding = System.Text.Encoding.UTF8,
//            };
//            message.To.Add(dto.Receiver.Email);

//            var notificationEntity = new NotiNotificationDetail
//            {
//                Title = dto.Title,
//                Content = dto.AltContent,
//                IsRead = false,
//                Receiver = dto.Receiver.Email,
//                ReceiverId = dto.Receiver.UserId ?? 0,
//            };

//            await _notificationRepository.AddAsync(notificationEntity);
//            await _notificationRepository.SaveChangesAsync();

//            try
//            {
//                await smtpClient.SendMailAsync(message);

//                _logger.LogInformation("Email sent successfully to {Email}", dto.Receiver.Email);

//                await _notificationLogService.LogSuccessAsync(notificationEntity.Id, dto.Receiver.Email);
//            }
//            catch (FormatException ex)
//            {
//                _logger.LogError(ex, "Invalid email format: {Email}", dto.Receiver.Email);

//                await _notificationLogService.LogFailureAsync(notificationEntity.Id, dto.Receiver.Email, NotificationErrorCode.EmailAddressInvalid);
//                throw new NotificationException(NotificationErrorCode.EmailAddressInvalid, NotificationErrorCode.Names[NotificationErrorCode.EmailAddressInvalid]);
//            }
//            catch (SmtpFailedRecipientsException ex)
//            {
//                _logger.LogError(ex, "Multiple recipient failures, detail: {Errors}", JsonSerializer.Serialize(ex.InnerExceptions));

//                await _notificationLogService.LogFailureAsync(notificationEntity.Id, dto.Receiver.Email, NotificationErrorCode.SmtpSendError);
//                throw new NotificationException(NotificationErrorCode.SmtpSendError, NotificationErrorCode.Names[NotificationErrorCode.SmtpSendError]);
//            }
//            catch (SmtpFailedRecipientException ex)
//            {
//                _logger.LogError(ex, "Recipient address rejected: {Email}", dto.Receiver.Email);

//                await _notificationLogService.LogFailureAsync(notificationEntity.Id, dto.Receiver.Email, NotificationErrorCode.SmtpConfigError);
//                throw new NotificationException(NotificationErrorCode.SmtpConfigError, NotificationErrorCode.Names[NotificationErrorCode.SmtpConfigError]);
//            }
//            catch (SmtpException ex)
//            {
//                _logger.LogError(ex, "SMTP Error while sending to {Email}", dto.Receiver.Email);

//                await _notificationLogService.LogFailureAsync(notificationEntity.Id, dto.Receiver.Email, NotificationErrorCode.SmtpError);
//                throw new NotificationException(NotificationErrorCode.SmtpError, NotificationErrorCode.Names[NotificationErrorCode.SmtpError]);
//            }
//            catch (AuthenticationException ex)
//            {
//                _logger.LogError(ex, "SMTP Authentication failed");

//                await _notificationLogService.LogFailureAsync(notificationEntity.Id, dto.Receiver.Email, NotificationErrorCode.SmtpAuthError);
//                throw new NotificationException(NotificationErrorCode.SmtpAuthError, NotificationErrorCode.Names[NotificationErrorCode.SmtpAuthError]);
//            }
//            catch (TaskCanceledException ex)
//            {
//                _logger.LogError(ex, "SMTP Timeout while sending to {Email}", dto.Receiver.Email);

//                await _notificationLogService.LogFailureAsync(notificationEntity.Id, dto.Receiver.Email, NotificationErrorCode.SmtpTimeoutError);
//                throw new NotificationException(NotificationErrorCode.SmtpTimeoutError, NotificationErrorCode.Names[NotificationErrorCode.SmtpTimeoutError]);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Unknown error while sending to {Email}", dto.Receiver.Email);

//                await _notificationLogService.LogFailureAsync(notificationEntity.Id, dto.Receiver.Email, NotificationErrorCode.NotiBaseError);
//                throw new NotificationException(NotificationErrorCode.NotiBaseError, NotificationErrorCode.Names[NotificationErrorCode.NotiBaseError]);
//            }
//        }
//    }
//}
