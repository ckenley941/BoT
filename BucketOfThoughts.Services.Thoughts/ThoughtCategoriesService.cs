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
    public class ThoughtCategoriesService : BaseService<ThoughtCategory>
    {
        private readonly ThoughtsDbContext _dbContext;
        private readonly IMapper _mapper;
        public ThoughtCategoriesService(ICrudRepository<ThoughtCategory> repository, IDistributedCache cache, ThoughtsDbContext dbContext, IMapper mapper) : base (repository, cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ThoughtCategoryDto>> GetAsync()
        {
            var thoughtCategories = (await base.GetFromCacheAsync(CacheKeys.ThoughtCategories));
            var dictThoughtCategories = thoughtCategories.ToDictionary(x => x.ThoughtCategoryId, x => x.Description);

            var categories = new List<ThoughtCategoryDto>();
            thoughtCategories.ToList().ForEach(x =>
            {
                dictThoughtCategories.TryGetValue(x.ParentId ?? 0, out string? categoryDescription);
                categories.Add(new ThoughtCategoryDto()
                {
                    Id = x.ThoughtCategoryId,
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

        public async Task<ThoughtCategory> UpdateAsync2(ThoughtCategoryDto updateItem)
        {
            var dbItem = await _repository.GetByIdAsync(updateItem.Id);
            if (dbItem == null)
            {
                throw new Exception("Not found."); //Make this custom not found exception
            }

            _mapper.Map(updateItem, dbItem);

            await base.UpdateAsync(dbItem);
            await _cache.RemoveAsync(CacheKeys.ThoughtCategories);

            return dbItem;
        }

        private async Task<int> GetDefaultModuleId()
        {
            int defaultModuleId = await _cache.GetRecordAsync<int>(CacheKeys.DefaultModuleId);
            if (defaultModuleId <= 0)
            {
                defaultModuleId = _dbContext.ThoughtModules.FirstOrDefault(x => x.Description == "Other")?.ThoughtModuleId ?? 0;
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