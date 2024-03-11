using Doczy.Business.HelperServices.BackgroundServices;
using Doczy.Business.HelperServices.Implementations;
using Doczy.Business.HelperServices.Interfaces;
using Doczy.Business.Services.Implementations;
using Doczy.Business.Services.Interfaces;
using Doczy.Business.Validations.UnivercityValidation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Doczy.Business.Services
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IWorkPlaceService, WorkPlaceService>();
            services.AddScoped<IExperianceService,ExperianceService >();
            services.AddScoped<IOTPService,OTPService >();
            services.AddScoped<IUnivercityService,UnivercityService>(); 
            services.AddScoped<IDoctorRaitingService,DoctorRaitingService>();
            services.AddScoped<IFavoriteDoctorService,FavoriteDoctorService>();
            services.AddScoped<IEducationService,EducationService>();
            services.AddScoped<IFieldOfStudyService,FieldOfStudyService>();
            services.AddScoped<IAppointmentService,AppointmentService>();

            services.AddHostedService<ExpiredOTPCleanupService>();
            services.AddValidatorsFromAssemblyContaining<CreateUnivercityDtoValidation>();
            //services.AddScoped<IOTPCleanupService, OTPCleanupService>();
            //services.AddHostedService<OTPCleanupBackgroundService>();


        }
    }
}
