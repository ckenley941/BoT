using BucketOfThoughts.Api.Handlers.Outdoors;
using BucketOfThoughts.Api.Handlers.ThoughtBuckets;
using BucketOfThoughts.Api.Handlers.Thoughts;
using BucketOfThoughts.Core.Infrastructure.Constants;
using BucketOfThoughts.Core.Infrastructure.Enums;
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
            services.AddScoped<IThoughtBucketsRepository, ThoughtBucketsRepository>();
            services.AddScoped<IThoughtsService, ThoughtsService>();
            //services.AddScoped<ICrudRepository<Thought>, ThoughtsRepository>();
            services.AddScoped<IThoughtsRepository, ThoughtsRepository>();
            

            services.AddScoped<GetWebsiteLinksHandler>();
            services.AddScoped<GetRelatedThoughtsHandler>();
            services.AddScoped<GetThoughtByIdHandler>();
            services.AddScoped<GetThoughtsGridHandler>();
            services.AddScoped<InsertThoughtHandler>();

            services.AddScoped<GetThoughtBucketsHandler>();
            services.AddScoped<InsertThoughtBucketHandler>();
            services.AddScoped<UpdateThoughtBucketHandler>();
            services.AddScoped<DeleteThoughtBucketHandler>();


            services.AddScoped<GetOutdoorActivitiesHandler>();

            return services;
        }

        public static WebApplication AddThoughtApiEndpoints(this WebApplication app)
        {
            app.MapGet("/api/thoughts/random",
              async (GetWebsiteLinksHandler handler) =>
                  await handler.HandleAsync()
                  is ThoughtDto randomThought
                  ? Results.Ok(randomThought)
                  : Results.NotFound()
              );

            app.MapGet("/api/thoughts/grid",
             async (GetThoughtsGridHandler handler) =>
                 await handler.HandleAsync()
                 is IEnumerable<ThoughtGridDto> thoughts
                 ? Results.Ok(thoughts)
                 : Results.NotFound()
             );

            app.MapGet("/api/thoughts/{id}",
             async (GetThoughtByIdHandler handler, int id) =>
                 await handler.HandleAsync(id)
                  is ThoughtDto thought
                 ? Results.Ok(thought)
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
                 is IEnumerable<ThoughtGridDto> thoughts
                 ? Results.Ok(thoughts)
                 : Results.NotFound()
             );

            app.MapGet("/api/thoughtbuckets",
                async (GetThoughtBucketsHandler handler) =>
                   await handler.HandleAsync()
                   is IEnumerable<ThoughtBucketDto> thoughtBuckets
                   ? Results.Ok(thoughtBuckets)
                   : Results.NotFound()
                );

            app.MapPost("/api/thoughtbucket",
               async (InsertThoughtBucketHandler handler, ThoughtBucket newItem) =>
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

            app.MapGet("/api/outdoorsactivities",
                (GetOutdoorActivitiesHandler handler) =>
                 handler.Handle()
                 is IEnumerable<string> outdoorActivities
                 ? Results.Ok(outdoorActivities)
                 : Results.NotFound()
              );

            return app;
        }
    }
}
