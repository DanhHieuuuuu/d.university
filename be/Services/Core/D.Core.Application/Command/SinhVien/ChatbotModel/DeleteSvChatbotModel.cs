using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.InfrastructureBase.Shared;
using D.Notification.ApplicationService.Abstracts;
using D.Notification.Domain.Enums;
using D.Notification.Dtos;
using Microsoft.AspNetCore.Http;

namespace D.Core.Application.Command.SinhVien.ChatbotModel
{
    public class DeleteSvChatbotModel : ICommandHandler<DeleteSvChatbotModelDto, bool>
    {
        private readonly ISvChatbotModelRepository _repository;
        private readonly INotificationService _notificationService;
        private IHttpContextAccessor _httpContextAccessor;

        public DeleteSvChatbotModel(
            ISvChatbotModelRepository repository,
            INotificationService notificationService,
            IHttpContextAccessor httpContext)
        {
            _repository = repository;
            _notificationService = notificationService;
            _httpContextAccessor = httpContext;
        }

        public async Task<bool> Handle(
            DeleteSvChatbotModelDto req,
            CancellationToken cancellationToken
        )
        {
            var entity = await _repository.GetByIdAsync(req.Id);
            if (entity == null)
            {
                throw new Exception($"Không tìm thấy ChatbotModel với Id = {req.Id}");
            }

            var modelName = entity.Name;
            var modelModelName = entity.ModelName;

            _repository.Delete(entity);
            await _repository.SaveChangeAsync();
            var userId = CommonUntil.GetCurrentUserId(_httpContextAccessor);

            // Send notification
            await _notificationService.SendAsync(new NotificationMessage
            {
                Receiver = new Receiver { UserId = userId },
                Title = "Xóa Chatbot Model",
                Content = $"Chatbot Model '{modelName}' đã được xóa.",
                AltContent = $"Chatbot Model '{modelName}' ({modelModelName}) đã được xóa khỏi hệ thống.",
                Channel = NotificationChannel.Realtime
            });

            return true;
        }
    }
}
