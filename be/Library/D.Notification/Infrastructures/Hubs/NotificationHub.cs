using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace D.Notification.Infrastructure.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Console.WriteLine("UserIdentifier = " + Context.UserIdentifier);
        }

        // Client gọi để join theo user
        public async Task JoinUser(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }
    }
}
