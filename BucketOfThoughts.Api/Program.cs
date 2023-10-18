using BucketOfThoughts.Api.Handlers;
using BucketOfThoughts.Core.Infrastructure;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.AddMiddleware();

app.Run();
