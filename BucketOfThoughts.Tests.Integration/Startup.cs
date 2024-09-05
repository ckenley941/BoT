using BucketOfThoughts.Core.Infrastructure.Constants;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BucketOfThoughts.Tests.Integration
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ThoughtsDbContext>(
                options =>
                  options.UseSqlServer("Server=LAPTOP-GM3PDTL4;Database=BoT-dev;Trusted_Connection=True;TrustServerCertificate=true;",//configuration.GetConnectionString(ConnectionStrings.BucketOfThoughts),
                  b => b.MigrationsAssembly(typeof(ThoughtsDbContext).Assembly.FullName)),
                ServiceLifetime.Scoped);
        }

        public void ConfigureHost(IHostBuilder hostBuilder) => hostBuilder
     .ConfigureHostConfiguration(builder => { })
     .ConfigureAppConfiguration((context, builder) => { });
    }
}
