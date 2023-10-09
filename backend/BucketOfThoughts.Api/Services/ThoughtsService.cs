using Thoughts.Data.SqlServer;
using BucketOfThoughts.Api.Infrastructure;
using EnsenaMe.Data.Contexts;
using Common.Data.Objects.Thoughts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BucketOfThoughts.Api.Services
{
    public class ThoughtsService : IThoughtsService
    {
        private readonly BucketOfThoughtsProdContext _dbContext;
        private readonly IDistributedCache _cache;
        public ThoughtsService(BucketOfThoughtsProdContext dbContext, IDistributedCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<Thought> GetRandomThoughtAsync()
        {
            //Eventually remove from cache what has already been used so we don't repeat random thoughts or added a flag
            var thoughts = await _cache.GetRecordAsync<List<Thought>>("Thoughts");

            if (thoughts == null)
            {
                thoughts = _dbContext.Thoughts
                    .Include(x => x.ThoughtDetails)
                    .Include(x => x.ThoughtCategory)
                    //.Include(x => x.ThoughtFiles)
                    .ToList();
                await _cache.SetRecordAsync("Thoughts", thoughts);
            }


            if (thoughts?.Count <= 0)
            {
                throw new Exception("Thoughts not found"); //TODO make custom not found exception passing in name of object not found "{} not found"
            }

            var rand = new Random();
            return thoughts[rand.Next(thoughts.Count)];
        }

        public async Task<List<ThoughtCategory>> GetThoughtCategoriesAsync()
        {
            var thougtCategories = await _cache.GetRecordAsync<List<ThoughtCategory>>("ThoughtCategories");

            if (thougtCategories == null)
            {
                thougtCategories = _dbContext.ThoughtCategories.ToList();
                await _cache.SetRecordAsync("ThoughtCategories", thougtCategories);
            }

            return thougtCategories;
        }

        public async Task<Thought> AddThoughtAsync(InsertThoughtDto newThought)
        {
            var thought = new Thought()
            {
                Description = newThought.Description,
                ThoughtCategoryId = newThought.ThoughtCategoryId
            };

            if (newThought.Details?.Count > 0)
            {
                int sortOrder = 0;
                foreach (var detail in newThought.Details)
                {
                    sortOrder++;
                    thought.ThoughtDetails.Add(
                        new ThoughtDetail()
                        {
                            Description = detail,
                            SortOrder = sortOrder
                        });
                }
            }

            _dbContext.Thoughts.Add(thought);
            await _dbContext.SaveChangesAsync();

            return thought;
        }
    }
}
