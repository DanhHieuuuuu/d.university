using D.ApplicationBase;
using D.Core.Domain.Dtos.Noti;
using D.InfrastructureBase.Shared;
using D.Notification.ApplicationService.Abstracts;
using Microsoft.AspNetCore.Http;

namespace D.Core.Application.Query.Noti
{
    public class FindPagingMyNotification
        : IQueryHandler<NotiRequestDto, PageResultNotificationDto<NotiResponseDto>>
    {
        private readonly INotificationService _notiService;
        private readonly IHttpContextAccessor _httpContext;

        public FindPagingMyNotification(
            INotificationService notiService,
            IHttpContextAccessor httpContext
        )
        {
            _notiService = notiService;
            _httpContext = httpContext;
        }

        public async Task<PageResultNotificationDto<NotiResponseDto>> Handle(
            NotiRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContext);
            var query = await _notiService.GetUserNotificationsAsync(userId);

            var result = new PageResultNotificationDto<NotiResponseDto>();

            result.TotalUnread = query.Where(n => !n.IsRead).Count();
            result.TotalItem = query.Count();

            if (request.Short)
            {
                query = query.Take(7);
            }
            else
            {
                query = query.Skip(request.SkipCount()).Take(request.PageSize);
            }

            var items = query
                .Select(n => new NotiResponseDto
                {
                    Id = n.Id,
                    Title = n.Content,
                    CreatedAt = n.CreatedAt,
                    IsRead = n.IsRead,
                })
                .ToList();

            result.Items = items;

            return result;
        }
    }
}
