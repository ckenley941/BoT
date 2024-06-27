using BucketOfThoughts.Api.Handlers.Words;
using BucketOfThoughts.Core.Infrastructure.Constants;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Data;
using BucketOfThoughts.Services.Languages.Objects;
using BucketOfThoughts.Services.Music.Data;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Api.Handlers.Extensions
{
    public static class MusicServiceExtensions
    {
        public static IServiceCollection AddMusicServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MusicDbContext>(
             options =>
               options.UseSqlServer(configuration.GetConnectionString(ConnectionStrings.BucketOfThoughts),
               b => b.MigrationsAssembly(typeof(MusicDbContext).Assembly.FullName)),
             ServiceLifetime.Scoped);

            return services;
        }
    }
}
