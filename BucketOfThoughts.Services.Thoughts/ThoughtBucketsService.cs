﻿using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Constants;
using BucketOfThoughts.Core.Infrastructure.Extensions;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtBucketsService : BaseCRUDService<ThoughtBucket, ThoughtBucketDto>
    {
        private readonly ThoughtsDbContext _dbContext;
        public ThoughtBucketsService(ThoughtsDbContext dbContext, IDistributedCache cache, IMapper mapper) : base (dbContext, cache, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ThoughtBucketDto>> GetAsync()
        {
            var thoughtBuckets = (await base.GetFromCacheAsync(CacheKeys.ThoughtBuckets));
            var dictThoughtBuckets = thoughtBuckets.ToDictionary(x => x.Id, x => x.Description);

            var categories = new List<ThoughtBucketDto>();
            thoughtBuckets.ToList().ForEach(x =>
            {
                dictThoughtBuckets.TryGetValue(x.ParentId ?? 0, out string? bucketDescription);
                categories.Add(new ThoughtBucketDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    SortOrder = x.SortOrder,
                    //ParentBucket = bucketDescription,
                    ParentId = x.ParentId,
                    ThoughtModuleId = x.ThoughtModuleId
                });
            });

            return categories;
        }

        public async Task<ThoughtBucket> InsertAsync(ThoughtBucketDto newItem)
        {
            //Set default ThoughtModuleId if not supplied by user
            newItem.ThoughtModuleId = newItem.ThoughtModuleId == 0 ? await GetDefaultModuleId() : newItem.ThoughtModuleId;
            var itemToAdd = await base.InsertDtoAsync(newItem, true, CacheKeys.ThoughtBuckets);
            return itemToAdd;
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