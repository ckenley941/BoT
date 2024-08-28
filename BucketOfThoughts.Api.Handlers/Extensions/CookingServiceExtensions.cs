using BucketOfThoughts.Core.Infrastructure.Constants;
using BucketOfThoughts.Services.Music.Data;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Api.Handlers.Extensions
{
    public static class CookingServiceExtensions
    {
        public static IServiceCollection AddCookingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CookingDbContext>(
             options =>
               options.UseSqlServer(configuration.GetConnectionString(ConnectionStrings.BucketOfThoughts),
               b => b.MigrationsAssembly(typeof(CookingDbContext).Assembly.FullName)),
             ServiceLifetime.Scoped);

            return services;
        }
    }
}
