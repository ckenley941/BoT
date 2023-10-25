using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.Extensions.Caching.Distributed;
using BucketOfThoughts.Services.Thoughts.Objects;
using BucketOfThoughts.Core.Infrastructure.Extensions;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Constants;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtCategoriesService : BaseService<ThoughtCategory>
    {
        private readonly ThoughtsDbContext _dbContext;
        public ThoughtCategoriesService(ICrudRepository<ThoughtCategory> repository, IDistributedCache cache, ThoughtsDbContext dbContext) : base (repository, cache)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ThoughtCategory>> GetAsync()
        {
            var thougtCategories = await base.GetFromCacheAsync(CacheKeys.ThoughtCategories);
            return thougtCategories;
        }

        public override async Task<ThoughtCategory> InsertAsync(ThoughtCategory newItem)
        {
            //Set default ThoughtModuleId if not supplied by user
            newItem.ThoughtModuleId = newItem.ThoughtModuleId == 0 ? await GetDefaultModuleId() : newItem.ThoughtModuleId;

            await base.InsertAsync(newItem);
            await _cache.RemoveAsync(CacheKeys.ThoughtCategories);

            return newItem;
        }

        public async Task<int> GetDefaultModuleId()
        {
            int? defaultModuleId = await _cache.GetRecordAsync<int>(CacheKeys.DefaultModuleId);
            if (defaultModuleId == null)
            {
                defaultModuleId = _dbContext.ThoughtModules.FirstOrDefault(x => x.Description == "Other")?.ThoughtModuleId ?? 0;
                if (defaultModuleId <= 0)
                {
                    throw new Exception("This shouldn't happen!");
                }
                await _cache.SetRecordAsync(CacheKeys.DefaultModuleId, defaultModuleId);
            }

            return defaultModuleId ?? 0;
        }
    }
}