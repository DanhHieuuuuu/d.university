using AutoMapper;
using D.Core.Domain.Dtos.Kpi.KpiChat;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;


namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiChatService : ServiceBase, IKpiChatService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IKpiLogStatusService _logKpiService;
        private readonly IKpiCaNhanService _kpiCaNhanService;
        private readonly IKpiDonViService _kpiDonViService;
        private readonly IKpiTruongService _kpiTruongService;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        public KpiChatService(
            ILogger<KpiChatService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IKpiLogStatusService logKpiService,
            IKpiCaNhanService kpiCaNhanService,
            IKpiDonViService kpiDonViService,
            IKpiTruongService kpiTruongService,
            IConfiguration configuration
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _logKpiService = logKpiService;
            _kpiCaNhanService = kpiCaNhanService;
            _kpiDonViService = kpiDonViService;
            _kpiTruongService = kpiTruongService;
            _configuration = configuration;
            _apiKey = _configuration["DifyConfig:ApiKey"];
            _baseUrl = _configuration["DifyConfig:BaseUrl"];
        }

        public async Task<string> GetKpiContextForChat(int userId)
        {
            var userRoles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                                .Where(x => x.IdNhanSu == userId).ToListAsync();

            bool isHieuTruong = userRoles.Any(r => r.Role == "HIEU_TRUONG" || r.Role == "CHU_TICH_HOI_DONG_TRUONG");
            List<int>? managedDonViIds = isHieuTruong ? null :
                userRoles.Where(r => r.IdDonVi.HasValue).Select(r => r.IdDonVi!.Value).Distinct().ToList();

            var finalContext = new
            {
                KpiCapTruong = isHieuTruong ? await _kpiTruongService.GetKpiTruongContextForAi() : null,
                KpiCacDonVi = (isHieuTruong || (managedDonViIds?.Any() ?? false))
                                ? await _kpiDonViService.GetKpiDonViContextForAi(managedDonViIds) : null,
                KpiCaNhan = await _kpiCaNhanService.GetKpiCaNhanContextForAi(userId, await _kpiCaNhanService.GetAllowedUserIds(userId))
            };

            return JsonSerializer.Serialize(finalContext, new JsonSerializerOptions
            {
                WriteIndented = false, 
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }

        public async Task<string> AskKpiQuestion(string userQuery)
        {
            try
            {
                var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
                string kpiContext = await GetKpiContextForChat(userId);
                _logger.LogInformation("--- SENDING JSON CONTEXT TO DIFY ---\n{0}", kpiContext);

                var requestBody = new
                {
                    inputs = new { kpi_data = kpiContext },
                    query = userQuery,
                    user = $"user_{userId}",
                    response_mode = "blocking"
                };

                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(120);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                var response = await client.PostAsJsonAsync(_baseUrl, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DifyChatResponse>();
                    return result?.Answer ?? "AI không có phản hồi.";
                }
                var errorCode = (int)response.StatusCode;
                var errorContent = await response.Content.ReadAsStringAsync();

                if (errorCode == 429)
                {
                    return "Hệ thống AI đang quá tải hoặc hết hạn mức (Quota). Vui lòng thử lại sau ít phút.";
                }

                _logger.LogError($"Dify API Error: {errorCode} - {errorContent}");
                return $"Có lỗi xảy ra khi kết nối với AI (Mã lỗi: {errorCode}).";
            }
            catch (TaskCanceledException)
            {
                return "AI phản hồi quá lâu, vui lòng thử lại với câu hỏi ngắn gọn hơn.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi nghiêm trọng khi gọi Dify Service");
                return "Hệ thống bận, vui lòng thử lại sau.";
            }
        }
    }
}
