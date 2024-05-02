using Doczy.DataAccess.Abstractions.Common;
using Doczy.DataAccess.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Doczy.DataAccess.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DoczyContext>
    {
        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DoczyContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Doczy.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DoczyContext>();
            var connectionString = configuration.GetConnectionString("Default");
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Doczy.DataAccess"));

            // Create an instance of AuditableEntitySaveChangesInterceptor
            var auditableInterceptor = new AuditableEntitySaveChangesInterceptor(_dateTime, _httpContextAccessor);

            // Now pass it to the constructor of DoczyContext
            return new DoczyContext(optionsBuilder.Options, auditableInterceptor);
        }
    }
}
