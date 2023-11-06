using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;
using BucketOfThoughts.Api.Handlers.ThoughtCategories;
using BucketOfThoughts.Api.Handlers.Thoughts;
using Microsoft.EntityFrameworkCore;
using BucketOfThoughts.Core.Infrastructure.Constants;

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
                ServiceLifetime.Transient);

            services.AddScoped<ThoughtCategoriesService>();
            services.AddScoped<ICrudRepository<ThoughtCategory>, ThoughtCategoriesRepository>();
            services.AddScoped<ThoughtsService>();
            services.AddScoped<ICrudRepository<Thought>, ThoughtsRepository>();
            services.AddScoped<GetRandomThoughtHandler>();
            services.AddScoped<GetThoughtCategoriesHandler>();
            services.AddScoped<GetThoughtsHandler>();
            services.AddScoped<InsertThoughtCategoryHandler>();
            services.AddScoped<InsertThoughtHandler>();
            return services;
        }

        public static WebApplication AddThoughtApiEndpoints(this WebApplication app)
        {
            app.MapGet("/api/thoughts/random",
              async (GetRandomThoughtHandler handler) =>
                  await handler.HandleAsync()
                  is Thought randomThought
                  ? Results.Ok(randomThought)
                  : Results.NotFound()
              );

            app.MapGet("/api/thoughts",
             async (GetThoughtsHandler handler) =>
                 await handler.HandleAsync()
                 is IEnumerable<ThoughtGridDto> thoughts
                 ? Results.Ok(thoughts)
                 : Results.NotFound()
             );

            app.MapPost("/api/thoughts",
                async (InsertThoughtHandler handler, InsertThoughtDto newItem) =>
                {
                    var thought = await handler.HandleAsync(newItem);
                    Results.Created($"/api/thoughts/{thought.ThoughtId}", thought);
                }
                );

            app.MapGet("/api/thoughtcategories",
                async (GetThoughtCategoriesHandler handler) =>
                   await handler.HandleAsync()
                   is IEnumerable<ThoughtCategory> thoughtCategories
                   ? Results.Ok(thoughtCategories)
                   : Results.NotFound()
                );

            app.MapPost("/api/thoughtcategory",
               async (InsertThoughtCategoryHandler handler, ThoughtCategory newItem) =>
               {
                   var thoughtCategory = await handler.HandleAsync(newItem);
                   Results.Created($"/api/thoughtcategories/{thoughtCategory.ThoughtCategoryId}", thoughtCategory);
               }
               );

            //app.MapPut("/api/thoughtcategory",
            //   async (UpdateThoughtCategoryHandler handler, ThoughtCategory updateItem) =>
            //   {
            //       await handler.HandleAsync(updateItem);
            //       Results.Ok(updateItem);
            //   }
            //   );

            //app.MapDelete("/api/thoughtcategory",
            //   async (DeleteThoughtCategoryHandler handler, ThoughtCategory deleteItem) =>
            //   {
            //       await handler.HandleAsync(deleteItem);
            //       Results.Ok();
            //   }
            //   );

            return app;
        }
    }
}
