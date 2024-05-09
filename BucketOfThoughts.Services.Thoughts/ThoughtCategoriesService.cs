using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.Extensions.Caching.Distributed;
using BucketOfThoughts.Services.Thoughts.Objects;
using BucketOfThoughts.Core.Infrastructure.Extensions;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Constants;
using Azure;
using AutoMapper;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtCategoriesService : BaseService<ThoughtCategory, ThoughtCategoryDto>
    {
        private readonly ThoughtsDbContext _dbContext;
        public ThoughtCategoriesService(IThoughtCategoriesRepository repository, IDistributedCache cache, ThoughtsDbContext dbContext, IMapper mapper) : base (repository, cache, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ThoughtCategoryDto>> GetAsync()
        {
            var thoughtCategories = (await base.GetFromCacheAsync(CacheKeys.ThoughtCategories));
            var dictThoughtCategories = thoughtCategories.ToDictionary(x => x.Id, x => x.Description);

            var categories = new List<ThoughtCategoryDto>();
            thoughtCategories.ToList().ForEach(x =>
            {
                dictThoughtCategories.TryGetValue(x.ParentId ?? 0, out string? categoryDescription);
                categories.Add(new ThoughtCategoryDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    SortOrder = x.SortOrder,
                    //ParentCategory = categoryDescription,
                    ParentId = x.ParentId,
                    ThoughtModuleId = x.ThoughtModuleId
                });
            });

            return categories;
        }

        public override async Task<ThoughtCategory> InsertAsync(ThoughtCategory newItem)
        {
            //Set default ThoughtModuleId if not supplied by user
            newItem.ThoughtModuleId = newItem.ThoughtModuleId == 0 ? await GetDefaultModuleId() : newItem.ThoughtModuleId;

            await base.InsertAsync(newItem);
            await _cache.RemoveAsync(CacheKeys.ThoughtCategories);

            return newItem;
        }


        private async Task<int> GetDefaultModuleId()
        {
            int defaultModuleId = await _cache.GetRecordAsync<int>(CacheKeys.DefaultModuleId);
            if (defaultModuleId <= 0)
            {
                defaultModuleId = _dbContext.ThoughtModules.FirstOrDefault(x => x.Description == "Thought")?.Id ?? 0;
                if (defaultModuleId <= 0)
                {
                    throw new Exception("This shouldn't happen!");
                }
                await _cache.SetRecordAsync(CacheKeys.DefaultModuleId, defaultModuleId);
            }

            return defaultModuleId;
        }
    }
}