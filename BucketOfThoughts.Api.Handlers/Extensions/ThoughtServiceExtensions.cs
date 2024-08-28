using BucketOfThoughts.Api.Handlers.Outdoors;
using BucketOfThoughts.Api.Handlers.ThoughtBuckets;
using BucketOfThoughts.Api.Handlers.Thoughts;
using BucketOfThoughts.Core.Infrastructure.Constants;
using BucketOfThoughts.Services.Music;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Api.Handlers.Extensions
{
    public static class ThoughtServiceExtensions
    {
        public static IServiceCollection AddThoughtServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ThoughtsDbContext>(
                options =>
                  options.UseSqlServer(configuration.GetConnectionString(ConnectionStrings.BucketOfThoughts),
                  b => b.MigrationsAssembly(typeof(ThoughtsDbContext).Assembly.FullName)),
                ServiceLifetime.Scoped);

            services.AddScoped<ThoughtBucketsService>();
            services.AddScoped<IThoughtsService, ThoughtsService>();
            services.AddScoped<OutdoorActivityLogService>();


            services.AddScoped<GetWebsiteLinksHandler>();
            services.AddScoped<GetRelatedThoughtsHandler>();
            services.AddScoped<GetThoughtByIdHandler>();
            services.AddScoped<GetThoughtsGridHandler>();
            services.AddScoped<InsertThoughtHandler>();

            services.AddScoped<GetThoughtBucketsHandler>();
            services.AddScoped<InsertThoughtBucketHandler>();
            services.AddScoped<UpdateThoughtBucketHandler>();
            services.AddScoped<DeleteThoughtBucketHandler>();

            services.AddScoped<GetOutdoorActivityTypesHandler>();
            services.AddScoped<GetOutdoorActivityLogsHandler>();
            services.AddScoped<InsertOutdoorActivityLogHandler>();

            return services;
        }

        public static WebApplication AddThoughtApiEndpoints(this WebApplication app)
        {
            app.MapGet("/api/thoughts/random",
              async (GetWebsiteLinksHandler handler, int? thoughtBucketId) =>
                  await handler.HandleAsync(thoughtBucketId)
                  is ThoughtDto item
                  ? Results.Ok(item)
                  : Results.NotFound()
              );

            app.MapGet("/api/thoughts/grid",
             async (GetThoughtsGridHandler handler) =>
                 await handler.HandleAsync()
                 is IEnumerable<ThoughtGridDto> data
                 ? Results.Ok(data)
                 : Results.NotFound()
             );

            app.MapGet("/api/thoughts/{id}",
             async (GetThoughtByIdHandler handler, int id) =>
                 await handler.HandleAsync(id)
                  is ThoughtDto item
                 ? Results.Ok(item)
                 : Results.NotFound()
             );

            app.MapPost("/api/thoughts",
                async (InsertThoughtHandler handler, InsertThoughtDto newItem) =>
                {
                    var thought = await handler.HandleAsync(newItem);
                    Results.Created($"/api/thoughts/{thought.Id}", thought);
                }
                );

            app.MapGet("/api/thoughts/related/{id}",
             async (GetRelatedThoughtsHandler handler, int id) =>
                await handler.HandleAsync(id)
                 is IEnumerable<ThoughtGridDto> data
                 ? Results.Ok(data)
                 : Results.NotFound()
             );

            app.MapGet("/api/thoughtbuckets",
                async (GetThoughtBucketsHandler handler) =>
                   await handler.HandleAsync()
                   is IEnumerable<ThoughtBucketDto> data
                   ? Results.Ok(data)
                   : Results.NotFound()
                );

            app.MapPost("/api/thoughtbucket",
               async (InsertThoughtBucketHandler handler, ThoughtBucketDto newItem) =>
               {
                   var thoughtBucket = await handler.HandleAsync(newItem);
                   Results.Created($"/api/thoughtbuckets/{thoughtBucket.Id}", thoughtBucket);
               }
               );

            app.MapPut("/api/thoughtbucket/{id}",
               async (UpdateThoughtBucketHandler handler, ThoughtBucketDto updateItem, int id) =>
               {
                   if (id != updateItem.Id)
                   {
                       Results.BadRequest();
                   }
                   await handler.HandleAsync(updateItem);
                   Results.Ok(updateItem);
               }
               );

            app.MapDelete("/api/thoughtbucket/{id}",
               async (DeleteThoughtBucketHandler handler, int id) =>
               {
                   await handler.HandleAsync(id);
                   Results.Ok();
               }
               );

            app.MapGet("/api/outdooractivitytypes",
                (GetOutdoorActivityTypesHandler handler) =>
                 handler.Handle()
                 is IEnumerable<string> data
                 ? Results.Ok(data)
                 : Results.NotFound()
              );

            app.MapGet("/api/outdooractivitylogs",
              async (GetOutdoorActivityLogsHandler handler) =>
                await handler.HandleAsync()
                 is IEnumerable<OutdoorActivityLogDto> data
                 ? Results.Ok(data)
                 : Results.NotFound()
             );

            //app.MapGet("/api/outdooractivitysummary",
            // async (GetOutdoorActivitySummaryHandler handler, DateOnly dateFrom, DateOnly dateTo, [FromBody] List<string> activityTypes) =>
            //   await handler.HandleAsync(dateFrom, dateTo, activityTypes)
            //    is IEnumerable<OutdoorActivitySummaryDto> data
            //    ? Results.Ok(data)
            //    : Results.NotFound()
            //);

            app.MapPost("/api/outdooractivitylog",
              async (InsertOutdoorActivityLogHandler handler, OutdoorActivityLogDto newItem) =>
              {
                  var outdoorActivityLog = await handler.HandleAsync(newItem);
                  Results.Created($"/api/outdooractivitylog/{outdoorActivityLog.Id}", outdoorActivityLog);
              }
            );

            return app;
        }
    }
}
