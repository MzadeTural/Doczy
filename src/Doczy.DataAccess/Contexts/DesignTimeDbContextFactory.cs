
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Doczy.DataAccess.Contexts
{
    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DoczyContext>
    //{
    //    public DoczyContext CreateDbContext(string[] args)
    //    {
    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Doczy.API"))
    //            .AddJsonFile("appsettings.json")
    //            .Build();

    //        var optionsBuilder = new DbContextOptionsBuilder<DoczyContext>();
    //        var connectionString = configuration.GetConnectionString("Default");
    //        optionsBuilder.UseSqlServer (connectionString, b => b.MigrationsAssembly("Doczy.DataAccess"));

    //        return new DoczyContext(optionsBuilder.Options);
    //    }
    //}  
}
