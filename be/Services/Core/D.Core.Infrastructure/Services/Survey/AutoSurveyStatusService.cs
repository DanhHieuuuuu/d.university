using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Survey
{
    public class AutoSurveyStatusService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AutoSurveyStatusService> _logger;
        public AutoSurveyStatusService(
            IServiceScopeFactory scopeFactory,
            ILogger<AutoSurveyStatusService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Auto Survey Status Service started");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var surveyService = scope.ServiceProvider.GetRequiredService<ISurveyService>();

                    await surveyService.ProcessAutoStatusUpdateAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in Auto Survey Status Service");
                }
                // loop in 5m
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
