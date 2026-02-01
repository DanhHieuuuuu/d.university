using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Domain.Dtos.SinhVien.ThongTinChiTiet;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.EntityFrameworkCore;
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
        private readonly ISvChatbotModelRepository _chatbotModelRepository;
        private readonly ISvSinhVienRepository _sinhVienRepository;
        private readonly ISvSinhVienService _sinhVienService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ChatbotService(
            ILogger<ChatbotService> logger,
            ISvChatbotHistoryRepository chatbotHistoryRepository,
            ISvChatbotModelRepository chatbotModelRepository,
            ISvSinhVienRepository sinhVienRepository,
            ISvSinhVienService sinhVienService,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _chatbotHistoryRepository = chatbotHistoryRepository;
            _chatbotModelRepository = chatbotModelRepository;
            _sinhVienRepository = sinhVienRepository;
            _sinhVienService = sinhVienService;
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

            // Lấy model đang được chọn
            var selectedModel = await _chatbotModelRepository.GetSelectedAsync();
            LlmConfigDto? llmConfig = null;
            if (selectedModel != null)
            {
                llmConfig = new LlmConfigDto
                {
                    Name = selectedModel.Name,
                    BaseUrl = selectedModel.BaseURL,
                    Model = selectedModel.ModelName,
                    ApiKey = selectedModel.APIKey
                };
            }

            // Gọi Python Chatbot API
            var pythonApiUrl = _configuration["ChatbotApi:BaseUrl"] ?? "http://localhost:8000";
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(120);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            string apiEndpoint;
            string jsonContent;

            // Nếu có mssv, sử dụng API chat-with-mssv để filter theo sinh viên
            if (!string.IsNullOrEmpty(request.Mssv))
            {
                var pythonRequest = new PythonChatWithMssvRequest
                {
                    Message = request.Message,
                    Mssv = request.Mssv,
                    ConversationHistory = conversationHistory,
                    LlmConfig = llmConfig
                };
                jsonContent = JsonSerializer.Serialize(pythonRequest, jsonOptions);
                apiEndpoint = $"{pythonApiUrl}/api/chat-with-mssv";
            }
            else
            {
                var pythonRequest = new PythonChatRequestWithLlm
                {
                    Message = request.Message,
                    ConversationHistory = conversationHistory,
                    LlmConfig = llmConfig
                };
                jsonContent = JsonSerializer.Serialize(pythonRequest, jsonOptions);
                apiEndpoint = $"{pythonApiUrl}/api/chat";
            }

            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation($"Calling Python API: {apiEndpoint}");

            var response = await client.PostAsync(apiEndpoint, httpContent);
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

        public async Task<SyncStudentsResponseDto> SyncAllStudentsAsync()
        {
            _logger.LogInformation("SyncAllStudentsAsync called");

            try
            {
                // Lấy tất cả sinh viên
                var allStudents = await _sinhVienRepository.TableNoTracking.ToListAsync();
                _logger.LogInformation($"Found {allStudents.Count} students to sync");

                var studentsData = new List<StudentDataItem>();

                foreach (var sv in allStudents)
                {
                    if (string.IsNullOrEmpty(sv.Mssv)) continue;

                    try
                    {
                        // Lấy thông tin chi tiết của từng sinh viên
                        var thongTinChiTiet = await _sinhVienService.GetThongTinChiTiet(
                            new SvThongTinChiTietRequestDto { Mssv = sv.Mssv }
                        );

                        studentsData.Add(new StudentDataItem
                        {
                            Mssv = sv.Mssv,
                            Data = thongTinChiTiet
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning($"Failed to get detail for student {sv.Mssv}: {ex.Message}");
                    }
                }

                if (studentsData.Count == 0)
                {
                    return new SyncStudentsResponseDto
                    {
                        Success = false,
                        Message = "Không có sinh viên nào để đồng bộ",
                        TotalStudents = 0,
                        TotalChunks = 0
                    };
                }

                // Gọi Python API để sync
                var pythonApiUrl = _configuration["ChatbotApi:BaseUrl"] ?? "http://localhost:8000";
                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromMinutes(10);

                var syncRequest = new SyncStudentsRequestDto { Students = studentsData };
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                };
                var jsonContent = JsonSerializer.Serialize(syncRequest, jsonOptions);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                _logger.LogInformation($"Calling Python API: {pythonApiUrl}/api/sync-students with {studentsData.Count} students");

                var response = await client.PostAsync($"{pythonApiUrl}/api/sync-students", httpContent);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var syncResponse = JsonSerializer.Deserialize<SyncStudentsResponseDto>(responseContent, jsonOptions);

                _logger.LogInformation($"Sync completed: {syncResponse?.Message}");

                return syncResponse ?? new SyncStudentsResponseDto
                {
                    Success = true,
                    Message = "Đồng bộ thành công",
                    TotalStudents = studentsData.Count,
                    TotalChunks = 0
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"SyncAllStudentsAsync failed: {ex.Message}");
                throw;
            }
        }
    }
}
