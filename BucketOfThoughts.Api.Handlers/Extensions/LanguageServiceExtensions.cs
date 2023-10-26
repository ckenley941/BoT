using BucketOfThoughts.Api.Handlers.Words;
using BucketOfThoughts.Core.Infrastructure.Constants;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Data;
using BucketOfThoughts.Services.Languages.Objects;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Api.Handlers.Extensions
{
    public static class LanguageServiceExtensions
    {
        public static IServiceCollection AddLanguageServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LanguageDbContext>(
             options =>
               options.UseSqlServer(configuration.GetConnectionString(ConnectionStrings.BucketOfThoughts),
               b => b.MigrationsAssembly(typeof(LanguageDbContext).Assembly.FullName)),
             ServiceLifetime.Transient);

            services.AddScoped<WordsService>();
            services.AddScoped<ICrudRepository<Word>, WordsRepository>();
            services.AddScoped<GetRandomWordHandler>();
            services.AddScoped<GetWordRelationshipsHandler>();
            services.AddScoped<GetWordsHandler>();
            services.AddScoped<GetWordTranslationsHandler>();
            services.AddScoped<GetWordTranslationsWithContextHandler>();
            services.AddScoped<InsertWordHandler>();
            return services;
        }

        public static WebApplication AddLanguageApiEndpoints(this WebApplication app)
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
