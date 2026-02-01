using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.InfrastructureBase.Shared;
using D.Notification.ApplicationService.Abstracts;
using D.Notification.Domain.Enums;
using D.Notification.Dtos;
using D.Untils.DataUntils;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace D.Core.Application.Command.SinhVien.ChatbotModel
{
    public class CreateSvChatbotModel : ICommandHandler<CreateSvChatbotModelDto, SvChatbotModelResponseDto>
    {
        private readonly ISvChatbotModelRepository _repository;
        private readonly INotificationService _notificationService;
        private IHttpContextAccessor _httpContextAccessor;

        public CreateSvChatbotModel(
            ISvChatbotModelRepository repository,
            INotificationService notificationService,
            IHttpContextAccessor httpContext)
        {
            _repository = repository;
            _notificationService = notificationService;
            _httpContextAccessor = httpContext;
        }

        public async Task<SvChatbotModelResponseDto> Handle(
            CreateSvChatbotModelDto req,
            CancellationToken cancellationToken
        )
        {
            if (req.IsSelected)
            {
                await _repository.ResetAllSelectedAsync();
            }

            // Encrypt API key before saving
            var encryptedApiKey = !string.IsNullOrEmpty(req.APIKey) 
                ? AesEncryption.Encrypt(req.APIKey) 
                : req.APIKey;

            var entity = new SvChatbotModel
            {
                Name = req.Name,
                Description = req.Description,
                BaseURL = req.BaseURL,
                APIKey = encryptedApiKey,
                ModelName = req.ModelName,
                IsLocal = req.IsLocal,
                IsSelected = req.IsSelected
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangeAsync();
            var userId = CommonUntil.GetCurrentUserId(_httpContextAccessor);

            // Send notification
            await _notificationService.SendAsync(new NotificationMessage
            {
                Receiver = new Receiver { UserId = userId },
                Title = "Chatbot Model mới",
                Content = $"Chatbot Model '{entity.Name}' đã được tạo thành công.",
                AltContent = $"Chatbot Model '{entity.Name}' ({entity.ModelName}) đã được thêm vào hệ thống.",
                Channel = NotificationChannel.Realtime
            });

            return new SvChatbotModelResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                BaseURL = entity.BaseURL,
                APIKey = entity.APIKey,
                ModelName = entity.ModelName,
                IsLocal = entity.IsLocal,
                IsSelected = entity.IsSelected,
                CreatedDate = entity.CreatedDate
            };
        }
    }
}
