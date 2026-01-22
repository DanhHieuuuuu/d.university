using D.Constants.Core.Delegation;
using D.Notification.ApplicationService.Abstracts;
using D.Notification.Domain.Enums;
using D.Notification.Dtos;
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

        public async Task UpdateExpiredRecordsAsync(
            ServiceUnitOfWork uow,
            INotificationService notificationService)
        {
            var now = DateTime.Now;
            var tomorrow = now.AddDays(1);

            var delegations = await uow.iDelegationIncomingRepository.Table
                .Include(d => d.ReceptionTimes)
                .Where(d => d.Status != DelegationStatus.Expired)
                .ToListAsync();

            foreach (var delegation in delegations)
            {
                var latestReception = delegation.ReceptionTimes
                    .OrderByDescending(x => x.Date.ToDateTime(x.EndDate))
                    .FirstOrDefault();

                if (latestReception == null)
                    continue;

                var endDateTime = latestReception.Date.ToDateTime(latestReception.EndDate);

                // Sắp hết hạn
                if (!delegation.IsExpiryNotified
                    && endDateTime > now
                    && endDateTime <= tomorrow)
                {
                    await notificationService.SendAsync(new NotificationMessage
                    {
                        Receiver = new Receiver
                        {
                            UserId = delegation.IdStaffReception
                        },
                        Title = "Đoàn vào sắp hết hạn",
                        Content = "Đoàn vào bạn phụ trách sẽ hết hạn trong vòng 24 giờ.",
                        AltContent =
                            $"Đoàn {delegation.Name} ({delegation.Code}) sẽ hết hạn tiếp đoàn lúc {endDateTime:dd/MM/yyyy - HH:mm}",
                        Channel = NotificationChannel.Realtime
                    });

                    delegation.IsExpiryNotified = true;
                }

                // Đã hết hạn
                if (endDateTime <= now)
                {
                    delegation.Status = DelegationStatus.Expired;
                }
            }

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
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                    await UpdateExpiredRecordsAsync(uow, notificationService);
                    await UpdateExpiredContractsAsync(uow, notificationService);
                    _logger.LogInformation($"Auto update expired contracts.");

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

        public async Task UpdateExpiredContractsAsync(
            ServiceUnitOfWork uow,
            INotificationService notificationService)
        {
            var now = DateTime.Now;
            var one_month_ago = now.AddDays(-30);

            var contracts = await uow.iNsHopDongRepository.Table
                .Where(d => d.HopDongCoThoiHanDenNgay.HasValue)
                .ToListAsync();

            foreach (var hd in contracts)
            {
                var endDateTime = hd.HopDongCoThoiHanDenNgay;
                var nhansu = await uow.iNsNhanSuRepository.Table.FirstOrDefaultAsync(x => x.Id == hd.IdNhanSu);

                if (nhansu == null) 
                    continue;
                if (nhansu.IdHopDong != hd.Id)
                    continue;

                // Sắp hết hạn (trước 30 ngày)
                if (endDateTime <= one_month_ago)
                {
                    await notificationService.SendAsync(new NotificationMessage
                    {
                        Receiver = new Receiver
                        {
                            UserId = hd.IdNhanSu
                        },
                        Title = "Hợp đồng sắp hết hạn",
                        Content = $"Hợp đồng làm việc của bạn sẽ hết hạn vào {endDateTime:dd/MM/yyyy - HH:mm}.",
                        AltContent =
                            $"Hợp đồng làm việc với {nhansu.HoDem + " " + nhansu.Ten} sẽ đến hạn lúc {endDateTime:dd/MM/yyyy - HH:mm}.",
                        Channel = NotificationChannel.Realtime
                    });
                }

                // Đã hết hạn
                if (endDateTime <= now)
                {
                    nhansu.IsThoiViec = true;
                    nhansu.DaChamDutHopDong = true;                    
                }
            }

            await uow.SaveChangesAsync();
        }
    }
}
