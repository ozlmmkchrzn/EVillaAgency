using EVillaAgency.BusinessLayer.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class CouponBackgroundManager : ICouponBackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public CouponBackgroundManager(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var couponManager = scope.ServiceProvider.GetRequiredService<CouponManager>();
                    await couponManager.UpdateExpiredCouponsAsync();
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Her 24 saatte bir çalıştır
            }
        }
    }
}
