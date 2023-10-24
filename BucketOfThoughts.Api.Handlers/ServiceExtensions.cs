using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using BucketOfThoughts.Services.Thoughts.Objects;
using BucketOfThoughts.Api.Handlers.Thoughts;
using BucketOfThoughts.Api.Handlers.Words;
using BucketOfThoughts.Services.Languages.Objects;
using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Data;
using BucketOfThoughts.Api.Handlers.ThoughtCategories;

namespace BucketOfThoughts.Api.Handlers
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            // Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
            // package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
            services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

            services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddDbContext<ThoughtsDbContext>(
                options =>
                  options.UseSqlServer(configuration.GetConnectionString("BucketOfThoughtsConnection"),
                  b => b.MigrationsAssembly(typeof(ThoughtsDbContext).Assembly.FullName)),
                ServiceLifetime.Transient);

            services.AddDbContext<LanguageDbContext>(
               options =>
                 options.UseSqlServer(configuration.GetConnectionString("BucketOfThoughtsConnection"),
                 b => b.MigrationsAssembly(typeof(LanguageDbContext).Assembly.FullName)),
               ServiceLifetime.Transient);

            //Thoughts
            services.AddScoped<ThoughtCategoriesService>();
            services.AddScoped<ICrudRepository<ThoughtCategory>, ThoughtCategoriesRepository>();
            services.AddScoped<ThoughtsService>();
            services.AddScoped<ICrudRepository<Thought>, ThoughtsRepository>();
            services.AddScoped<GetRandomThoughtHandler>();
            services.AddScoped<GetThoughtCategoriesHandler>();
            services.AddScoped<GetThoughtsHandler>();
            services.AddScoped<InsertThoughtCategoryHandler>();
            services.AddScoped<InsertThoughtHandler>();

            //Words
            services.AddScoped<WordsService>();
            services.AddScoped<ICrudRepository<Word>, WordsRepository>();
            services.AddScoped<GetRandomWordHandler>();
            services.AddScoped<GetWordRelationshipsHandler>();
            services.AddScoped<GetWordsHandler>();
            services.AddScoped<GetWordTranslationsHandler>();
            services.AddScoped<GetWordTranslationsWithContextHandler>();
            services.AddScoped<InsertWordHandler>();

            services.AddDistributedMemoryCache();

            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            return services;
        }

        public static WebApplication AddMiddleware(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.AddMinimumApiEndpoints();

            app.UseCors("corsapp");
            return app;
        }

        private static WebApplication AddMinimumApiEndpoints(this WebApplication app)
        {
            app.MapGet("/health", () => "Health check");
            app.AddThoughtApiEndpoints();
            app.AddWordApiEndpoints();

            return app;
        }

        private static WebApplication AddThoughtApiEndpoints(this WebApplication app)
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
                 is IEnumerable<GetThoughtDto> thoughts
                 ? Results.Ok(thoughts)
                 : Results.NotFound()
             );

            app.MapGet("/api/thoughtcategories",
           async (GetThoughtCategoriesHandler handler) =>
               await handler.HandleAsync()
               is IEnumerable<ThoughtCategory> thoughtCategories
               ? Results.Ok(thoughtCategories)
               : Results.NotFound()
           );

            app.MapPost("/api/thoughts",
                async (InsertThoughtHandler handler, InsertThoughtDto newItem) =>
                {
                    var thought = await handler.HandleAsync(newItem);
                    Results.Created($"/api/thoughts/{thought.ThoughtId}", thought);
                }
                );

            app.MapPost("/api/thoughtcategory",
               async (InsertThoughtCategoryHandler handler, ThoughtCategory newItem) =>
               {
                   var thoughtCategory = await handler.HandleAsync(newItem);
                   Results.Created($"/api/thoughtcategories/{thoughtCategory.ThoughtCategoryId}", thoughtCategory);
               }
               );

            return app;
        }

        private static WebApplication AddWordApiEndpoints(this WebApplication app)
        {
            app.MapGet("/api/words/random",
             async (GetRandomWordHandler handler) =>
                 await handler.HandleAsync()
                 is WordTranslationDto randomWord
                 ? Results.Ok(randomWord)
                 : Results.NotFound()
             );

            app.MapGet("/api/words",
             async (GetWordsHandler handler) =>
                 await handler.HandleAsync()
                 is IEnumerable<GetWordDto> words
                 ? Results.Ok(words)
                 : Results.NotFound()
             );

            app.MapGet("/api/words/GetTranslations/{id}",
            async (GetWordTranslationsHandler handler, int id) =>
                await handler.HandleAsync(id)
                is List<WordDto> wordTranslations
                ? Results.Ok(wordTranslations)
                : Results.NotFound()
            );

            app.MapGet("/api/words/GetTranslationsWithContext/{id}",
            async (GetWordTranslationsWithContextHandler handler, int id) =>
              await handler.HandleAsync(id)
              is List<WordContextDto> wordTranslations
              ? Results.Ok(wordTranslations)
              : Results.NotFound()
            );

            app.MapGet("/api/words/GetWordRelationships/{id}",
            async (GetWordRelationshipsHandler handler, int id) =>
              await handler.HandleAsync(id)
              is List<WordRelationshipDto> wordRelationships
              ? Results.Ok(wordRelationships)
              : Results.NotFound()
            );

            app.MapPost("/api/words",
               async (InsertWordHandler handler, InsertWordCardDto newItem) =>
               {
                   var wordId = await handler.HandleAsync(newItem);
                   Results.Created($"/api/words/wordId", newItem);
               }
               );

            return app;
        }
    }
}
