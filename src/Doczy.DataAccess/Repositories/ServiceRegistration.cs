
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Configurations;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Implementations.Base;
using Doczy.DataAccess.Repositories.Interfaces;
using Doczy.DataAccess.Repositories.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Doczy.DataAccess.Repositories
{
    public static class ServiceRegistration
    {
        public static void AddDataAccesServices(this IServiceCollection services)
        {
            services.AddDbContext<DoczyContext>(opt =>
            {
                opt.UseSqlServer(ServiceConfiguration.ConnectionString());
            }).AddIdentity<BaseAppUser, IdentityRole<Guid>>(x =>
            {
                x.Password.RequiredLength = 8;
                x.SignIn.RequireConfirmedEmail = true;
                x.User.RequireUniqueEmail = true;

            }).
            AddEntityFrameworkStores<DoczyContext>().AddDefaultTokenProviders();
            services.AddScoped<IWorkPlaceRepository, WorkPlaceRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorCategoryRepository, DoctorCategoryRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IServiceTypeRepository,ServiceTypeRepository>();
            services.AddScoped<IServiceRepository,ServiceRepository>();
            services.AddScoped<ILanguageRepository,LanguageRepository>();
            services.AddScoped<IDoctorLanguageRepository,DoctorLanguageRepository>();
            services.AddScoped<IExperianceRepository,ExperianceRepository>();
            services.AddScoped<IBaseAppUserRepository,BaseAppUserRepository>();
         
           


        }

    }
}
