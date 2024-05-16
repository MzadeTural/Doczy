using Doczy.Business.Services.Interfaces;
using Doczy.DataAccess.Abstractions.Common;
using Doczy.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Doczy.Business.HelperServices.BackgroundServices
{

    public class GenerateApoointmentMeetLinkService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDateTime _dateTime;



        public GenerateApoointmentMeetLinkService(IServiceScopeFactory serviceScopeFactory, IDateTime dateTime)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            _dateTime = dateTime;

        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DoczyContext>();
                var videoMeetingService = scope.ServiceProvider.GetRequiredService<IVideoMeetingService>();
                var currentTimeMinus15Minutes = DateTime.Now.AddMinutes(15);
                var expiredUsers = await dbContext.Appointments
                    .Include(u => u.Doctor)
                    .Include(u => u.Patient)
                    .Include(u => u.Service)
                    .ToListAsync();
                expiredUsers = expiredUsers.Where(u => (u.AppointmentDate.Date + u.AppointmentTime) <= currentTimeMinus15Minutes && u.MeetLink is null).ToList();
                //foreach (var user in expiredUsers)
                //{
                //    user.MeetLink = await videoMeetingService.CreateZoomAsync(user.Patient.FirstName + user.Doctor.FirstName, user.Service.Duration, user.AppointmentDate.ToString(), user.AppointmentTime.ToString());
                //    await Console.Out.WriteLineAsync("asda");
                //}
                await dbContext.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

