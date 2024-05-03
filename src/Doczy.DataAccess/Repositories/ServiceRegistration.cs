
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Abstractions.Common.Implementations;
using Doczy.DataAccess.Abstractions.Common;
using Doczy.DataAccess.Configurations;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Interceptors;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Implementations.Base;
using Doczy.DataAccess.Repositories.Interfaces;
using Doczy.DataAccess.Repositories.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Doczy.DataAccess.Repositories
{
    public static class ServiceRegistration
    {
        public static void AddDataAccesServices(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();
            services.AddDbContext<DoczyContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
            }).AddIdentity<BaseAppUser, IdentityRole<Guid>>(x =>
            {
                x.Password.RequiredLength = 8;
                x.SignIn.RequireConfirmedEmail = true;
                x.User.RequireUniqueEmail = true;

            }).
            AddEntityFrameworkStores<DoczyContext>().AddDefaultTokenProviders();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorCategoryRepository, DoctorCategoryRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IServiceTypeRepository,ServiceTypeRepository>();
            services.AddScoped<IServiceRepository,ServiceRepository>();
            services.AddScoped<ILanguageRepository,LanguageRepository>();
            services.AddScoped<IDoctorLanguageRepository,DoctorLanguageRepository>();
            services.AddScoped<IExperianceRepository,ExperianceRepository>();
            services.AddScoped<IBaseAppUserRepository,BaseAppUserRepository>();
            services.AddScoped<IUnivercityRepository,UnivercityRepository>();
            services.AddScoped<IFavoriteDoctorRepository,FavoriteDoctorRepository>();
            services.AddScoped<IDoctorRatingRepository,DoctorRatingRepository>();
            services.AddScoped<IEducationRepository,EducationRepository>();
            services.AddScoped<IFieldOfStudyRepository,FieldOfStudyRepository>();
            services.AddScoped<IAppointmentRepository,AppointmentRepository>();
            services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            services.AddScoped<IAvailableHourRepository, AvailableHourRepository>();
            services.AddScoped<ITempAppointmentRepository, TempAppointmentRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<ICategorySliderRepository, CategorySliderRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IUnivercityDegreeRepository, UnivercityDegreeRepository>();
            services.AddScoped<IPayriffPaymentRepository, PayriffPaymentRepository>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        }

    }
}
