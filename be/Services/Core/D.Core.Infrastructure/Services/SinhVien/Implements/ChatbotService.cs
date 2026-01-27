using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.SinhVien.Implements
{
    public class ChatbotService : IChatbotService
    {
        private readonly ILogger<ChatbotService> _logger;
        private readonly ISvChatbotHistoryRepository _chatbotHistoryRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ChatbotService(
            ILogger<ChatbotService> logger,
            ISvChatbotHistoryRepository chatbotHistoryRepository,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _chatbotHistoryRepository = chatbotHistoryRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ChatbotResponseDto> ChatAsync(ChatbotRequestDto request)
        {
            _logger.LogInformation($"ChatAsync called. SessionId: {request.SessionId}, Mssv: {request.Mssv}");

            var sessionId = request.SessionId;
            var isNewSession = string.IsNullOrEmpty(sessionId);

            if (isNewSession)
            {
                sessionId = Guid.NewGuid().ToString();
            }

            // Lấy lịch sử chat theo sessionId
            var historyList = await _chatbotHistoryRepository.GetBySessionIdAsync(sessionId!);

            // Chuyển đổi sang conversation_history format
            var conversationHistory = historyList
                .Where(x => !string.IsNullOrEmpty(x.Role) && !string.IsNullOrEmpty(x.Content))
                .Select(x => new ConversationHistoryItem
                {
                    Role = x.Role?.ToLower(),
                    Content = x.Content
                })
                .ToList();

            // Gọi Python Chatbot API
            var pythonApiUrl = _configuration["ChatbotApi:BaseUrl"] ?? "http://localhost:8000";
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(120);

            var pythonRequest = new PythonChatRequest
            {
                Message = request.Message,
                ConversationHistory = conversationHistory
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };
            var jsonContent = JsonSerializer.Serialize(pythonRequest, jsonOptions);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation($"Calling Python API: {pythonApiUrl}/api/chat");

            var response = await client.PostAsync($"{pythonApiUrl}/api/chat", httpContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var pythonResponse = JsonSerializer.Deserialize<PythonChatResponse>(responseContent, jsonOptions);

            // Lưu message của user vào lịch sử
            var userHistory = new D.Core.Domain.Entities.SinhVien.SvChatbotHistory
            {
                SessionId = sessionId,
                Mssv = request.Mssv,
                Title = isNewSession ? request.Message?.Substring(0, Math.Min(50, request.Message?.Length ?? 0)) : null,
                Role = "user",
                Content = request.Message,
                LastAccess = DateTime.Now
            };
            await _chatbotHistoryRepository.AddAsync(userHistory);

            // Lưu response của assistant vào lịch sử
            var assistantHistory = new D.Core.Domain.Entities.SinhVien.SvChatbotHistory
            {
                SessionId = sessionId,
                Mssv = request.Mssv,
                Role = "assistant",
                Content = pythonResponse?.Response,
                LastAccess = DateTime.Now
            };
            await _chatbotHistoryRepository.AddAsync(assistantHistory);
            await _chatbotHistoryRepository.SaveChangeAsync();

            _logger.LogInformation($"Chat completed. SessionId: {sessionId}");

            return new ChatbotResponseDto
            {
                Response = pythonResponse?.Response,
                ContextUsed = pythonResponse?.Context_used,
                RewrittenQuery = pythonResponse?.Rewritten_query,
                SessionId = sessionId
            };
        }
    }
}
