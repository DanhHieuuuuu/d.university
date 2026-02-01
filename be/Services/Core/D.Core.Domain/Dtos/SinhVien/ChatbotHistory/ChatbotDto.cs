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

    public class StudentDataItem
    {
        public string? Mssv { get; set; }
        public object? Data { get; set; }
    }

    public class SyncStudentsRequestDto
    {
        public List<StudentDataItem>? Students { get; set; }
    }

    public class SyncStudentsResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int TotalStudents { get; set; }
        public int TotalChunks { get; set; }
    }

    public class PythonChatWithMssvRequest
    {
        public string? Message { get; set; }
        public string? Mssv { get; set; }
        public List<ConversationHistoryItem>? ConversationHistory { get; set; }
        public LlmConfigDto? LlmConfig { get; set; }
    }

    public class PythonChatRequestWithLlm
    {
        public string? Message { get; set; }
        public List<ConversationHistoryItem>? ConversationHistory { get; set; }
        public LlmConfigDto? LlmConfig { get; set; }
    }

    public class LlmConfigDto
    {
        public string? Name { get; set; }
        public string? BaseUrl { get; set; }
        public string? Model { get; set; }
        public string? ApiKey { get; set; }
    }
}
