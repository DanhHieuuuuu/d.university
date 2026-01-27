namespace D.Core.Domain.Dtos.SinhVien.ChatbotHistory
{
    public class ChatbotRequestDto
    {
        public string? Message { get; set; }
        public string? SessionId { get; set; }
        public string? Mssv { get; set; }
    }

    public class ChatbotResponseDto
    {
        public string? Response { get; set; }
        public List<string>? ContextUsed { get; set; }
        public string? RewrittenQuery { get; set; }
        public string? SessionId { get; set; }
    }

    public class ConversationHistoryItem
    {
        public string? Role { get; set; }
        public string? Content { get; set; }
    }

    public class PythonChatRequest
    {
        public string? Message { get; set; }
        public List<ConversationHistoryItem>? ConversationHistory { get; set; }
    }

    public class PythonChatResponse
    {
        public string? Response { get; set; }
        public List<string>? Context_used { get; set; }
        public string? Rewritten_query { get; set; }
    }
}
