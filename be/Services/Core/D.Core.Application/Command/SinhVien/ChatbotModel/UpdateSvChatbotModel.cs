using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.InfrastructureBase.Shared;
using D.Notification.ApplicationService.Abstracts;
using D.Notification.Domain.Enums;
using D.Notification.Dtos;
using D.Untils.DataUntils;
using Microsoft.AspNetCore.Http;

namespace D.Core.Application.Command.SinhVien.ChatbotModel
{
    public class UpdateSvChatbotModel : ICommandHandler<UpdateSvChatbotModelDto, SvChatbotModelResponseDto>
    {
        private readonly ISvChatbotModelRepository _repository;
        private readonly INotificationService _notificationService;
        private IHttpContextAccessor _httpContextAccessor;
        public UpdateSvChatbotModel(
            ISvChatbotModelRepository repository,
            INotificationService notificationService,
            IHttpContextAccessor httpContext)
        {
            _repository = repository;
            _notificationService = notificationService;
            _httpContextAccessor = httpContext;
        }

        public async Task<SvChatbotModelResponseDto> Handle(
            UpdateSvChatbotModelDto req,
            CancellationToken cancellationToken
        )
        {
            var entity = await _repository.GetByIdAsync(req.Id);
            if (entity == null)
            {
                throw new Exception($"Không tìm thấy ChatbotModel với Id = {req.Id}");
            }

            // Chỉ update IsSelected nếu có giá trị và thay đổi từ false -> true
            if (req.IsSelected.HasValue && req.IsSelected.Value && !entity.IsSelected)
            {
                await _repository.ResetAllSelectedAsync();
            }

            // Chỉ update các trường có giá trị (không null)
            if (req.Name != null)
                entity.Name = req.Name;
            
            if (req.Description != null)
                entity.Description = req.Description;
            
            if (req.BaseURL != null)
                entity.BaseURL = req.BaseURL;
            
            // Encrypt API key nếu có giá trị mới
            if (req.APIKey != null)
                entity.APIKey = AesEncryption.Encrypt(req.APIKey);
            
            if (req.ModelName != null)
                entity.ModelName = req.ModelName;
            
            if (req.IsLocal.HasValue)
                entity.IsLocal = req.IsLocal.Value;
            
            if (req.IsSelected.HasValue)
                entity.IsSelected = req.IsSelected.Value;

            _repository.Update(entity);
            await _repository.SaveChangeAsync();
            var userId = CommonUntil.GetCurrentUserId(_httpContextAccessor);

            // Send notification
            await _notificationService.SendAsync(new NotificationMessage
            {
                Receiver = new Receiver { UserId = userId },
                Title = "Cập nhật Chatbot Model",
                Content = $"Chatbot Model '{entity.Name}' đã được cập nhật.",
                AltContent = $"Chatbot Model '{entity.Name}' ({entity.ModelName}) đã được cập nhật thành công.",
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
