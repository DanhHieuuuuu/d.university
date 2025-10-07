using D.Auth.Infrastructure.Services.Abstracts;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Services.Implements
{
    public class CoreApiClient : ICoreApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CoreApiClient> _logger;

        public CoreApiClient(HttpClient httpClient, ILogger<CoreApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<DmChucVuResponseDto?> GetChucVuNameAsync(int? id)
        {
            if (!id.HasValue) return null;

            try
            {
                var response = await _httpClient.GetAsync($"/api/DmChucVu/{id.Value}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Không lấy được chức vụ {id}. Status: {status}", id, response.StatusCode);
                    return null;
                }

                var result = await response.Content.ReadFromJsonAsync<DmChucVuResponseDto>();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi Core API lấy chức vụ {id}", id);
                return null;
            }
        }
    }

}
