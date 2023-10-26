﻿using BucketOfThoughts.Api.Handlers.Extensions;
using BucketOfThoughts.Api.Handlers.Words;
using BucketOfThoughts.Service.Dashboards;
using BucketOfThoughts.Services.Languages.Data;
using BucketOfThoughts.Services.Languages.Objects;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BucketOfThoughts.Api.Handlers.Extensions
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

            services.AddThoughtServices(configuration);
            services.AddLanguageServices(configuration);
            services.AddDashboardServices();
          

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
            app.AddLanguageApiEndpoints();
            app.AddDashoardApiEndpoints();

            return app;
        } 
    }
}
