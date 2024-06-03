using BucketOfThoughts.Api.Handlers.Outdoors;
using BucketOfThoughts.Api.Handlers.ThoughtCategories;
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

            services.AddScoped<ThoughtCategoriesService>();
            services.AddScoped<IThoughtCategoriesRepository, ThoughtCategoriesRepository>();
            services.AddScoped<IThoughtsService, ThoughtsService>();
            //services.AddScoped<ICrudRepository<Thought>, ThoughtsRepository>();
            services.AddScoped<IThoughtsRepository, ThoughtsRepository>();
            

            services.AddScoped<GetWebsiteLinksHandler>();
            services.AddScoped<GetRelatedThoughtsHandler>();
            services.AddScoped<GetThoughtByIdHandler>();
            services.AddScoped<GetThoughtsGridHandler>();
            services.AddScoped<InsertThoughtHandler>();

            services.AddScoped<GetThoughtCategoriesHandler>();
            services.AddScoped<InsertThoughtCategoryHandler>();
            services.AddScoped<UpdateThoughtCategoryHandler>();
            services.AddScoped<DeleteThoughtCategoryHandler>();


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

            app.MapGet("/api/thoughtcategories",
                async (GetThoughtCategoriesHandler handler) =>
                   await handler.HandleAsync()
                   is IEnumerable<ThoughtCategoryDto> thoughtCategories
                   ? Results.Ok(thoughtCategories)
                   : Results.NotFound()
                );

            app.MapPost("/api/thoughtcategory",
               async (InsertThoughtCategoryHandler handler, ThoughtCategory newItem) =>
               {
                   var thoughtCategory = await handler.HandleAsync(newItem);
                   Results.Created($"/api/thoughtcategories/{thoughtCategory.Id}", thoughtCategory);
               }
               );

            app.MapPut("/api/thoughtcategory/{id}",
               async (UpdateThoughtCategoryHandler handler, ThoughtCategoryDto updateItem, int id) =>
               {
                   if (id != updateItem.Id)
                   {
                       Results.BadRequest();
                   }
                   await handler.HandleAsync(updateItem);
                   Results.Ok(updateItem);
               }
               );

            app.MapDelete("/api/thoughtcategory/{id}",
               async (DeleteThoughtCategoryHandler handler, int id) =>
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
