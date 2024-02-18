



using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Configurations;
using Doczy.DataAccess.Contexts;
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
            }).AddIdentity<AppUser, IdentityRole<Guid>>(x =>
            {
                x.Password.RequiredLength = 8;
               
                x.SignIn.RequireConfirmedEmail = true;
                x.User.RequireUniqueEmail = true;
               
            }).
            AddEntityFrameworkStores<DoczyContext>().AddDefaultTokenProviders();

           

        }

    }
}
