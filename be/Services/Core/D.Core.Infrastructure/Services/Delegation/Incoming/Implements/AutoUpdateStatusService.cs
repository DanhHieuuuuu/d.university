using D.Constants.Core.Delegation;
using Microsoft.EntityFrameworkCore;
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

        public async Task UpdateExpiredRecordsAsync(ServiceUnitOfWork uow)
        {
            var now = DateTime.Now;

            // Lấy tất cả các delegation
            var delegations = await uow.iDelegationIncomingRepository.Table
                .Include(d => d.ReceptionTimes) 
                .ToListAsync();

            foreach (var delegation in delegations)
            {
                // Tìm bản ghi ReceptionTime gần nhất của delegation
                var latestReception = delegation.ReceptionTimes
                    .OrderByDescending(x => x.Date.ToDateTime(x.EndDate))
                    .FirstOrDefault();

                if (latestReception != null)
                {
                    var endDateTime = latestReception.Date.ToDateTime(latestReception.EndDate);

                    if (endDateTime <= now)
                    {
                        // Cập nhật trạng thái Delegation thành hết hạn
                        delegation.Status = DelegationStatus.Expired;

                        _logger.LogInformation(
                            $"Delegation {delegation.Id} đã hết hạn lúc {now}");
                    }
                }
            }

            // Lưu thay đổi vào DB
            await uow.SaveChangesAsync();
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
                    await UpdateExpiredRecordsAsync(uow);

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
