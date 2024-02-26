using Doczy.Business.Services.Implementations;
using Doczy.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Doczy.Business.Services
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
            services.AddScoped<ILanguageService, LanguageService>();

            services.AddScoped<IServiceService, ServiceService>();

        }
    }
}
