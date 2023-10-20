using BucketOfThoughts.Api.Handlers;
using BucketOfThoughts.FileService;
using BucketOfThoughts.Imports.Thoughts;
using BucketOfThoughts.Services;
using Common.Data.Objects.Thoughts;
using Common.Data.Objects.Words;
using EnsenaMe.Data.Contexts;
using EnsenaMe.Data.MongoDB;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace BucketOfThoughts.Core.Infrastructure
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
            //builder.Services.AddDbContext<AuthIdentityDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("Home"),
            //    b => b.MigrationsAssembly(typeof(AuthIdentityDbContext).Assembly.FullName)), ServiceLifetime.Transient);
            //builder.Services
            //    .AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AuthIdentityDbContext>()
            //    .AddDefaultTokenProviders();

            //builder.Services.AddAuthentication();


            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            builder.Services.AddApiServices(builder.Configuration);

            //Added for ThoughtsHandler serialization of child properties
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddDistributedMemoryCache();

            //builder.Services.AddStackExchangeRedisCache(options =>
            //{
            //    //options.InstanceName = "";
            //    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:RedisCache");
            //});

            return builder;
        }

        public static IServiceCollection AddApiServices (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EnsenaMeContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Home"),
                b => b.MigrationsAssembly(typeof(EnsenaMeContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddScoped<RandomWordHandler>();
            services.AddScoped<ThoughtsHandler>();

            services.AddScoped<IWordsService, WordsService>();
            services.AddScoped<ICsvProcessor, CsvProcessor>();
            services.AddScoped<IIngestionFileProcessor, ThoughtDataProcessor>();

            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDB"));
            services.AddSingleton<MongoDBService>();

            services.AddOutputCache();
            //services.AddSingleton<IDistributedCache>();

            return services;
        }

        public static WebApplication AddMiddleware(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCustomExceptionHandler();

            app.UseCors("corsapp");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.AddMinimumApiEndpoints();

            return app;
        }

        private static void AddMinimumApiEndpoints(this WebApplication app)
        {
            app.MapGet("/hello/",
                async (RandomWordHandler randomWordHandler, MongoDBService dbService) =>
                    await randomWordHandler.GetAsync()
                    is WordTranslationDto randomWord
                    ? Results.Ok(randomWord)
                    : Results.NotFound()
                );


            app.MapPost("/api/thoughts",
                async (ThoughtsHandler thoughtsHandler, InsertThoughtDto newThought) =>
                    {
                        //var thought = await thoughtsHandler.AddThoughtAsync(newThought);
                        //Results.Created($"/api/thoughts/{thought.ThoughtId}", thought);
                    }
                );
        }

        private static void UseCustomExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                //Eventually rewrite this to be more specific to my app - custom exception/logging/etc
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    // using static System.Net.Mime.MediaTypeNames;
                    context.Response.ContentType = Text.Plain;

                    await context.Response.WriteAsync("An exception was thrown.");

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                    {
                        await context.Response.WriteAsync(" The file was not found.");
                    }

                    if (exceptionHandlerPathFeature?.Path == "/")
                    {
                        await context.Response.WriteAsync(" Page: Home.");
                    }
                });
            });
        }
    }    
}   

