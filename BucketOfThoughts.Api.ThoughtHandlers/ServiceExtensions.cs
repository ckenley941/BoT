using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.ThoughtModule.Data;
using BucketOfThoughts.Services.ThoughtModule;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BucketOfThoughts.Api.ThoughtHandlers
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            // Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
            // package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
            services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

            services.AddDbContext<BucketOfThoughtsContext>(
                options =>
                  options.UseSqlServer(configuration.GetConnectionString("BucketOfThoughtsConnection"),
                  b => b.MigrationsAssembly(typeof(BucketOfThoughtsContext).Assembly.FullName)),
                ServiceLifetime.Transient);

            services.AddScoped<ThoughtsService>();
            services.AddScoped<ICrudRepository<Thought>, ThoughtsRepository>();

            services.AddScoped<GetRandomThoughtHandler>();
            services.AddScoped<GetThoughtsHandler>();
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

            app.MapGet("/api/newthoughts/random",
              async (GetRandomThoughtHandler handler) =>
                  await handler.HandleAsync()
                  is Thought randomThought
                  ? Results.Ok(randomThought)
                  : Results.NotFound()
              );

            app.MapGet("/api/newthoughts",
             async (GetThoughtsHandler handler) =>
                 await handler.HandleAsync()
                 is IEnumerable<Thought> thoughts
                 ? Results.Ok(thoughts)
                 : Results.NotFound()
             );

            return app;
        }
    }
}
