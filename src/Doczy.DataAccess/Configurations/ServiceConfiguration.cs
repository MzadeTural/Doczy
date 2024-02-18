using Microsoft.Extensions.Configuration;

namespace Doczy.DataAccess.Configurations
{
    public class ServiceConfiguration
    {
        public static string ConnectionString()
        {

            ConfigurationManager manager = new ConfigurationManager();
            manager.SetBasePath(Directory.GetCurrentDirectory() + "../../RentaCar.Api");
            manager.AddJsonFile("appsettings.json");
            return manager.GetConnectionString("Default");
        }
    }
}
