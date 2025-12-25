using D.Constants.Core.Delegation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements
{
    public class AutoUpdateStatusService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AutoUpdateStatusService> _logger;

        public AutoUpdateStatusService(
            IServiceScopeFactory scopeFactory,
            ILogger<AutoUpdateStatusService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var uow = scope.ServiceProvider.GetRequiredService<ServiceUnitOfWork>();

                    var now = DateTime.Now;

                    //var expiredList = await uow.iReceptionTimeRepository.TableNoTracking.Where(x => x.Date > now.Date);

                    //if (expiredList.Any())
                    //{
                    //    expiredList.ForEach(x => x.Status = DelegationStatus.Expired);

                    //    _logger.LogInformation($"Auto update {expiredList.Count} expired records");
                    //}
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Auto update status failed");
                }
                _logger.LogInformation($"Auto update expired records");
                // chạy mỗi 1 tiếng
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
