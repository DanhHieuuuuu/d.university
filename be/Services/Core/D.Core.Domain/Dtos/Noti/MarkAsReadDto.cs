using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Noti
{
    public class MarkAsReadDto : ICommand
    {
        public int NotificationId { get; set; }
    }
}
