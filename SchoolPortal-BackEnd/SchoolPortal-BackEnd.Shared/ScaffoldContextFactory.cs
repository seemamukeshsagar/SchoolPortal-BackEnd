using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Shared
{
    public class ScaffoldContextFactory : IDesignTimeDbContextFactory<SchoolNewPortalContext>
    {
        public SchoolNewPortalContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SchoolNewPortalContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.CommandTimeout(600); // 10 minutes timeout
                options.EnableRetryOnFailure();
            });

            return new SchoolNewPortalContext(optionsBuilder.Options);
        }
    }
}
