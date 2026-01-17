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
            var managerRoles = new[] { "PHO_HIEU_TRUONG", "TRUONG_DON_VI_CAP_2", "TRUONG_DON_VI_CAP_3" };

            var managedDonViIds = userRoles
                .Where(r => r.IdDonVi.HasValue && managerRoles.Contains(r.Role))
                .Select(r => r.IdDonVi!.Value)
                .Distinct()
                .ToList();

            bool hasManagerRights = isHieuTruong || managedDonViIds.Any();
            var fullContext = new StringBuilder();
            fullContext.AppendLine("# BÁO CÁO DỮ LIỆU KPI QUẢN LÝ");
            fullContext.AppendLine($" Thời gian: {DateTime.Now:HH:mm dd/MM/yyyy}");
            fullContext.AppendLine("---");
            if (isHieuTruong)
            {
                fullContext.AppendLine(await _kpiTruongService.GetKpiTruongContextForAi());
                fullContext.AppendLine("---");
            }

            if (hasManagerRights)
            {
                var queryDonViIds = isHieuTruong ? null : managedDonViIds;
                fullContext.AppendLine(await _kpiDonViService.GetKpiDonViContextForAi(queryDonViIds));
                fullContext.AppendLine("---");
            }
            var allowedStaffIds = await _kpiCaNhanService.GetAllowedUserIds(userId);
            fullContext.AppendLine(await _kpiCaNhanService.GetKpiCaNhanContextForAi(userId, allowedStaffIds));

            return fullContext.ToString();
        }

        public async Task<string> AskKpiQuestion(string userQuery)
        {
            try
            {
                var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
                string kpiMarkdownContext = await GetKpiContextForChat(userId);
                var requestBody = new
                {
                    inputs = new { kpi_data = kpiMarkdownContext },
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
                return errorCode == 429
                    ? "Hệ thống AI đang bận (Quota), vui lòng thử lại sau."
                    : $"Có lỗi xảy ra (Mã lỗi: {errorCode}).";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi nghiêm trọng khi gọi Dify Service");
                return "Hệ thống bận, vui lòng thử lại sau.";
            }
        }
    }
}
