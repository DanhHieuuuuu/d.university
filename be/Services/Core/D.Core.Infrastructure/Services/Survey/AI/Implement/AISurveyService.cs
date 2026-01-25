using D.Core.Domain.Dtos.Survey.AI;
using D.Core.Infrastructure.Services.Survey.AI.Abstracts;
using D.Core.Infrastructure.Services.Survey.Report.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Survey.AI.Implement
{
    public class AISurveyService : IAISurveyService
    {
        private readonly IReportSurveyService _reportService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AISurveyService> _logger;

        public AISurveyService(
            IReportSurveyService reportService,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<AISurveyService> logger)
        {
            _reportService = reportService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> AnalyzeSurveyWithAIAsync(int reportId)
        {
            try
            {
                _logger.LogInformation($"Getting survey data for report {reportId}");
                var surveyData = await _reportService.GetAIAnalysisDataAsync(reportId);

                _logger.LogInformation($"Calling Dify AI for report {reportId}");
                var aiResponses = await CallDifyAIAsync(surveyData);

                if (aiResponses == null || !aiResponses.Any())
                {
                    _logger.LogWarning($"No AI responses received for report {reportId}");
                    return false;
                }

                _logger.LogInformation($"Saving {aiResponses.Count} AI responses for report {reportId}");
                await _reportService.SaveAIResponseAsync(reportId, aiResponses);

                _logger.LogInformation($"Successfully completed AI analysis for report {reportId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error analyzing survey with AI for report {reportId}");
                throw;
            }
        }

        private async Task<List<AIReportDto>> CallDifyAIAsync(SurveyAIDataDto surveyData)
        {
            var apiKey = _configuration["Dify:ApiKey"];
            var apiUrl = _configuration["Dify:WorkflowUrl"] ?? "https://api.dify.ai/v1/workflows/run";

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("Dify API key is not configured");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear(); // Đảm bảo header sạch
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var payload = new
            {
                inputs = new
                {
                    full_data = JsonSerializer.Serialize(surveyData)
                },
                response_mode = "blocking",
                user = "system"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Dify AI response: {responseContent}");

            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var difyResponse = JsonSerializer.Deserialize<DifyWorkflowResponse>(responseContent, jsonOptions);

            var rawJson = difyResponse?.Data?.Outputs?.text?.ToString()
                          ?? difyResponse?.Data?.Outputs?.Result?.ToString();

            if (!string.IsNullOrEmpty(rawJson))
            {
                if (rawJson.Contains("```"))
                {
                    rawJson = System.Text.RegularExpressions.Regex.Replace(rawJson, @"^```[a-zA-Z]*\n?", "");
                    rawJson = rawJson.Replace("```", "").Trim();
                }

                try
                {
                    var aiResponses = JsonSerializer.Deserialize<List<AIReportDto>>(rawJson, jsonOptions);

                    if (aiResponses != null)
                    {
                        return aiResponses;
                    }
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error parse JSON  AI. {rawJson}", rawJson);
                }
            }

            _logger.LogWarning($"Response: {responseContent}");
            return new List<AIReportDto>();
        }

        private class DifyWorkflowResponse
        {
            public string WorkflowRunId { get; set; }
            public string TaskId { get; set; }
            public DifyData Data { get; set; }
        }

        private class DifyData
        {
            public string Id { get; set; }
            public string WorkflowId { get; set; }
            public string Status { get; set; }
            public DifyOutputs Outputs { get; set; }
        }

        private class DifyOutputs
        {
            public object text { get; set; }
            public object Result { get; set; }
        }
    }
}
