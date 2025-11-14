using D.ApplicationBase;
using D.Core.Domain.Dtos.Noti;
using D.Notification.ApplicationService.Abstracts;
using Microsoft.AspNetCore.Http;

namespace D.Core.Application.Query.Noti
{
    public class MarkAsReadHandler : ICommandHandler<MarkAsReadDto>
    {
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContext;

        public MarkAsReadHandler(
            INotificationService notificationService,
            IHttpContextAccessor httpContext
        )
        {
            _notificationService = notificationService;
            _httpContext = httpContext;
        }

        public async Task Handle(MarkAsReadDto request, CancellationToken cancellationToken)
        {
            await _notificationService.MarkAsReadAsync(request.NotificationId);
            return;
        }
    }
}
