using Microsoft.AspNetCore.Mvc;

namespace D.Notification.Domain.Common
{
    public class PagingRequestDto
    {
        [FromQuery(Name = "pageIndex")]
        public int PageIndex { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 10;

        public int SkipCount()
        {
            return (PageIndex - 1) * PageSize;
        }
    }
}
