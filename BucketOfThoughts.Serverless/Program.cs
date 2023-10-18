using BucketOfThoughts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Thoughts.Data.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

builder.Services.AddDbContext<BucketOfThoughtsProdContext>(
    //options =>
    //  options.UseSqlServer(configuration.GetConnectionString("Home"),
    //  b => b.MigrationsAssembly(typeof(BucketOfThoughtsProdContext).Assembly.FullName)), 
    ServiceLifetime.Transient);

builder.Services.AddScoped<ThoughtsService>();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.MapGet("/api/thoughts/random",
              async (ThoughtsService thoughtsService) =>
                  await thoughtsService.GetRandomThoughtAsync()
                  is Thought randomThought
                  ? Results.Ok(randomThought)
                  : Results.NotFound()
              );

app.Run();
