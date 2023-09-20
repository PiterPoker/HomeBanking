using HomeBanking.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace HomeBanking.API.Infrastructure.Factories
{
    public class HomeBankingDbContextFactory : IDesignTimeDbContextFactory<HomeBankingContext>
    {
        public HomeBankingContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<HomeBankingContext>();

            optionsBuilder.UseNpgsql(config["ConnectionString"],
                           npgsqlOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                           });
            return new HomeBankingContext(optionsBuilder.Options);
        }
    }
}
