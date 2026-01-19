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
using System.Text.Json.Serialization;


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
            var managerRoles = new[] { "PHO_HIEU_TRUONG", "TRUONG_DON_VI_CAP_2", "TRUONG_DON_VI_CAP_3" };

            var managedDonViIds = userRoles
                .Where(r => r.IdDonVi.HasValue && managerRoles.Contains(r.Role))
                .Select(r => r.IdDonVi!.Value)
                .Distinct()
                .ToList();

            bool hasManagerRights = isHieuTruong || managedDonViIds.Any();
            object? dataTruong = null;
            if (isHieuTruong)
            {
                dataTruong = await _kpiTruongService.GetKpiTruongContextForAi();
            }

            object? dataDonVi = null;
            if (hasManagerRights)
            {
                var queryDonViIds = isHieuTruong ? null : managedDonViIds;
                dataDonVi = await _kpiDonViService.GetKpiDonViContextForAi(queryDonViIds);
            }

            var allowedStaffIds = await _kpiCaNhanService.GetAllowedUserIds(userId);
            var dataCaNhan = await _kpiCaNhanService.GetKpiCaNhanContextForAi(userId, allowedStaffIds);
            var finalData = new
            {
                MetaData = new
                {
                    ThoiGianBaoCao = DateTime.Now.ToString("HH:mm dd/MM/yyyy"),
                    UserYeuCau = userId
                },
                KpiCapTruong = dataTruong,
                KpiCacDonVi = dataDonVi,
                KpiCaNhan = dataCaNhan
            };
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = false,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(finalData, jsonOptions);
        }

        public async Task<string> AskKpiQuestion(string userQuery)
        {
            try
            {
                var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
                string kpiJsonContext = await GetKpiContextForChat(userId);
                var requestBody = new
                {
                    inputs = new Dictionary<string, object>
            {
                { "kpi_data", kpiJsonContext }
            },
                    query = userQuery,
                    user = $"user_{userId}",
                    response_mode = "blocking",
                    conversation_id = ""
                };

                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                var jsonContent = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_baseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DifyChatResponse>();
                    return result?.Answer ?? "AI không có phản hồi.";
                }

                var errorCode = (int)response.StatusCode;
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Dify API Error: {errorCode} - Content: {errorContent}");
                _logger.LogError($"Payload sent: {jsonContent}"); 

                if (errorCode == 429) return "Hệ thống quá tải, vui lòng thử lại sau.";
                if (errorCode == 400 || errorCode == 404) return "Lỗi kết nối AI. Vui lòng liên hệ IT.";

                return $"Lỗi hệ thống ({errorCode}).";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception calling Dify");
                return "Đã xảy ra lỗi ngoại lệ.";
            }
        }
    }
}
