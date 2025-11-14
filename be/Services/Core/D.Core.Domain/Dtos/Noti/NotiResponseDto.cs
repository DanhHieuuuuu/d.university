namespace D.Core.Domain.Dtos.Noti
{
    public class NotiResponseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
