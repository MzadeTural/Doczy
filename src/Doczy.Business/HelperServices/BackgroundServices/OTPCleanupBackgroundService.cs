using Doczy.Business.HelperServices.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Doczy.Business.HelperServices.BackgroundServices
{
    public class OTPCleanupBackgroundService : BackgroundService
    {
        private readonly IOTPCleanupService _otpCleanupService;

        public OTPCleanupBackgroundService(IOTPCleanupService otpCleanupService)
        {
            _otpCleanupService = otpCleanupService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _otpCleanupService.CleanupExpiredOTPAsync();
                // Run cleanup every 5 minutes
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

    }
}
