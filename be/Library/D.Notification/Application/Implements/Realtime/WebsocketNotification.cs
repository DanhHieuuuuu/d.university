using D.Notification.Application.Abstracts.Realtime;
using D.Notification.Domain.Entities;
using D.Notification.Dtos;
using D.Notification.Infrastructure.Hubs;
using D.Notification.Infrastructures.Persistence;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Transactions;

namespace D.Notification.ApplicationService.Implements.Realtime
{
    public class WebsocketNotification : IRealtimeNotification
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;

        public WebsocketNotification(
            IHubContext<NotificationHub> hubContext,
            IServiceScopeFactory scopeFactory
        )
        {
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }

  
        public async Task SendRealtimeAsync(NotificationMessage message)
            {
                if (message.Receiver?.UserId == null) return;
                if (string.IsNullOrWhiteSpace(message.Content)) return;
                     
                using var suppressTx = new TransactionScope(
                    TransactionScopeOption.Suppress,
                    TransactionScopeAsyncFlowOption.Enabled
                );
        
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
                var userId = message.Receiver.UserId.Value.ToString();
                var entity = new NotiNotificationDetail
                {
                    ReceiverId = message.Receiver.UserId.Value,
                    Title = message.Title,
                    Content = message.AltContent ?? message.Content,
                    IsRead = false,
                    CreatedAt = DateTime.Now
                };
        
                await dbContext.AddAsync(entity);
                await dbContext.SaveChangesAsync();

                suppressTx.Complete();
                await _hubContext.Clients
                    .User(userId)
                    .SendAsync("ReceiveNotification", new RealtimeNotificationDto
                    {
                        Id = entity.Id,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedAt = entity.CreatedAt
                    });
        
            }
       
}
}
