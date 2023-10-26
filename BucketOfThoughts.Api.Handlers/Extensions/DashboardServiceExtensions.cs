using BucketOfThoughts.Api.Handlers.Words;
using BucketOfThoughts.Core.Infrastructure.Constants;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Languages.Data;
using BucketOfThoughts.Services.Languages.Objects;
using BucketOfThoughts.Services.Languages;
using Microsoft.EntityFrameworkCore;
using BucketOfThoughts.Service.Dashboards;
using BucketOfThoughts.Api.Handlers.Dashboards;
using BucketOfThoughts.Service.Dashboards.Objects;

namespace BucketOfThoughts.Api.Handlers.Extensions
{
    public static class DashboardServiceExtensions
    {
        public static IServiceCollection AddDashboardServices(this IServiceCollection services)
        {
            services.AddScoped<DashboardsService>();
            services.AddScoped<DashboardsServiceContainer>();

            services.AddScoped<GetSelectedDashboardHandler>();
            return services;
        }

        public static WebApplication AddDashoardApiEndpoints(this WebApplication app)
        {
            app.MapGet("/api/dashboards/selected",
                 async (GetSelectedDashboardHandler handler, string dashboardType) =>
                     await handler.HandleAsync(dashboardType)
                     is DashboardResponse results
                     ? Results.Ok(results)
                     : Results.NotFound()
                 );

            return app;
        }
    }
}
