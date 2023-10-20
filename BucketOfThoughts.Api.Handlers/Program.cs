using BucketOfThoughts.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.AddMiddleware();

app.Run();
