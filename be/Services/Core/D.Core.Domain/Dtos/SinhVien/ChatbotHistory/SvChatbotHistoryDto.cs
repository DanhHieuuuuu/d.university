using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.SinhVien.ChatbotHistory
{
    public class SvChatbotHistoryResponseDto
    {
        public int Id { get; set; }
        public string? SessionId { get; set; }
        public string? Mssv { get; set; }
        public string? Title { get; set; }
        public string? Role { get; set; }
        public string? Content { get; set; }
        public DateTime? LastAccess { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class CreateSvChatbotHistoryDto : ICommand<SvChatbotHistoryResponseDto>
    {
        public string? SessionId { get; set; }
        public string? Mssv { get; set; }
        public string? Title { get; set; }
        public string? Role { get; set; }
        public string? Content { get; set; }
    }

    public class UpdateSvChatbotHistoryDto : ICommand<SvChatbotHistoryResponseDto>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Role { get; set; }
        public string? Content { get; set; }
    }

    public class DeleteSvChatbotHistoryDto : ICommand<bool>
    {
        public int Id { get; set; }
    }

    public class GetSvChatbotHistoryBySessionDto : IQuery<List<SvChatbotHistoryResponseDto>>
    {
        public string? SessionId { get; set; }
    }

    public class GetSvChatbotHistoryByMssvDto : IQuery<List<SvChatbotHistoryResponseDto>>
    {
        public string? Mssv { get; set; }
    }

    public class GetSvChatbotSessionsDto : IQuery<List<SvChatbotHistoryResponseDto>>
    {
        public string? Mssv { get; set; }
    }
}
