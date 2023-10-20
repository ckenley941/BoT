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

            services.AddScoped<ThoughtsService>();
            services.AddScoped<ICrudRepository<Thought>, ThoughtsRepository>();
            services.AddScoped<WordsService>();
            services.AddScoped<ICrudRepository<Word>, WordsRepository>();

            services.AddScoped<GetRandomThoughtHandler>();
            services.AddScoped<GetThoughtsHandler>();
            services.AddScoped<AddThoughtHandler>();


            services.AddScoped<GetRandomWordHandler>();
            services.AddDistributedMemoryCache();

            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            return services;
        }
        
        public static WebApplication AddMinimumApiEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

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
                 is IEnumerable<Thought> thoughts
                 ? Results.Ok(thoughts)
                 : Results.NotFound()
             );

            app.MapPost("/api/thoughts",
                async (AddThoughtHandler thoughtsHandler, InsertThoughtDto newThought) =>
                {
                    var thought = await thoughtsHandler.HandleAsync(newThought);
                    Results.Created($"/api/thoughts/{thought.ThoughtId}", thought);
                }
                );

            app.MapGet("/api/words/random",
              async (GetRandomWordHandler handler) =>
                  await handler.HandleAsync()
                  is WordTranslationDto randomWord
                  ? Results.Ok(randomWord)
                  : Results.NotFound()
              );


            return app;
        }
    }
}
