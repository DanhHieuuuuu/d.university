namespace D.Core.Domain.Dtos.Noti
{
    public class PageResultNotificationDto<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalItem { get; set; }
        public int TotalUnread { get; set; }
    }
}
