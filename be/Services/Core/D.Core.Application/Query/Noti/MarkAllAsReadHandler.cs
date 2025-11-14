using D.ApplicationBase;
using D.Core.Domain.Dtos.Noti;
using D.InfrastructureBase.Shared;
using D.Notification.ApplicationService.Abstracts;
using Microsoft.AspNetCore.Http;

namespace D.Core.Application.Query.Noti
{
    public class MarkAllAsReadHandler : ICommandHandler<MarkAllAsReadDto>
    {
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContext;

        public MarkAllAsReadHandler(
            INotificationService notificationService,
            IHttpContextAccessor httpContext
        )
        {
            _notificationService = notificationService;
            _httpContext = httpContext;
        }

        public async Task Handle(MarkAllAsReadDto request, CancellationToken cancellationToken)
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContext);
            await _notificationService.MarkAllAsReadAsync(userId);
            return;
        }
    }
}
