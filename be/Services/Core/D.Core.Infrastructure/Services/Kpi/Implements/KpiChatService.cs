using AutoMapper;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiChat;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiChatService : ServiceBase, IKpiChatService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IKpiLogStatusService _logKpiService;
        private readonly IKpiCaNhanService _kpiCaNhanService;
        private readonly IKpiDonViService _kpiDonViService;
        private readonly IKpiTruongService _kpiTruongService;
        public KpiChatService(
            ILogger<KpiChatService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IKpiLogStatusService logKpiService,
            IKpiCaNhanService kpiCaNhanService,
            IKpiDonViService kpiDonViService,
            IKpiTruongService kpiTruongService
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _logKpiService = logKpiService;
            _kpiCaNhanService = kpiCaNhanService;
            _kpiDonViService = kpiDonViService;
            _kpiTruongService = kpiTruongService;
        }

        public async Task<string> GetKpiContextForChat(int userId)
        {
            var userRoles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                                .Where(x => x.IdNhanSu == userId).ToListAsync();

            var contextBuilder = new StringBuilder();
            contextBuilder.AppendLine("Dữ liệu KPI bạn được phép truy cập:");

            if (userRoles.Any(r => r.Role == "HIEU_TRUONG" || r.Role == "CHU_TICH_HOI_DONG_TRUONG"))
            {
                var kpiTruong =  _kpiTruongService.GetAllKpiTruong(new FilterKpiTruongDto { PageSize = 100 });
                contextBuilder.AppendLine("- KPI Trường: " + JsonSerializer.Serialize(kpiTruong));
            }

            var kpiDonVi = _kpiDonViService.GetAllKpiDonVi(new FilterKpiDonViDto { PageSize = 100 });
            if (kpiDonVi.TotalItem > 0)
            {
                contextBuilder.AppendLine("- KPI Các đơn vị quản lý: " + JsonSerializer.Serialize(kpiDonVi));
            }

            var kpiCaNhan = await _kpiCaNhanService.GetAllKpiCaNhan(new FilterKpiCaNhanDto { PageSize = 100 });
            if (kpiCaNhan.TotalItem > 0)
            {
                contextBuilder.AppendLine("- KPI Nhân sự cấp dưới: " + JsonSerializer.Serialize(kpiCaNhan));
            }

            return contextBuilder.ToString();
        }

        public async Task<string> AskKpiQuestion(string userQuery)
        {
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            string kpiContext = await GetKpiContextForChat(userId);
            var requestBody = new
            {
                inputs = new
                {
                    test_input = kpiContext, 
                    userinput = new
                    {        
                        query = userQuery,
                        files = new string[] { } 
                    }
                },
                query = userQuery,
                user = $"user_{userId}",
                response_mode = "blocking"
            };
            using var client = new HttpClient();
            //var apiKey = "app-X9zV8lo3Imn9UC7pt2MU6qwY";
            var apiKey = "app-8JynRrUdCAJabRXOTziG7DTr";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            try
            {
                var response = await client.PostAsJsonAsync("https://api.dify.ai/v1/chat-messages", requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DifyChatResponse>();
                    return result?.Answer ?? "AI không có phản hồi.";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Dify API Error: {error}");
                    return "Có lỗi xảy ra khi kết nối với AI (Lỗi API).";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi Dify Service");
                return "Hệ thống bận, vui lòng thử lại sau.";
            }
        }
    }
}
