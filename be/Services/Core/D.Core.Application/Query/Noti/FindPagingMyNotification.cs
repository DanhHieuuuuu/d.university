using D.ApplicationBase;
using D.Core.Domain.Dtos.Noti;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using D.InfrastructureBase.Shared;
using D.Notification.ApplicationService.Abstracts;
using Microsoft.AspNetCore.Http;

namespace D.Core.Application.Query.Noti
{
    public class FindPagingMyNotification
        : IQueryHandler<NotiRequestDto, PageResultDto<NotiResponseDto>>
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

        public async Task<PageResultDto<NotiResponseDto>> Handle(
            NotiRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContext);
            var query = await _notiService.GetUserNotificationsAsync(userId);

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

            var result = new PageResultDto<NotiResponseDto>();

            result.TotalItem = query.Count();
            result.Items = items;

            return result;
        }
    }
}
