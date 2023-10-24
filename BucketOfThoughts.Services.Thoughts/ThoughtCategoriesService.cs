using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.Extensions.Caching.Distributed;
using BucketOfThoughts.Services.Thoughts.Objects;
using BucketOfThoughts.Core.Infrastructure.Extensions;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtCategoriesService
    {
        private readonly ICrudRepository<ThoughtCategory> _repository;
        private readonly IDistributedCache _cache;
        public ThoughtCategoriesService(IDistributedCache cache, ICrudRepository<ThoughtCategory> repository)
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<IEnumerable<ThoughtCategory>> GetAsync()
        {
            var thougtCategories = await _cache.GetRecordAsync<IEnumerable<ThoughtCategory>>("ThoughtCategories");

            if (thougtCategories == null)
            {
                thougtCategories = await _repository.GetAsync();
                await _cache.SetRecordAsync("ThoughtCategories", thougtCategories);
            }

            return thougtCategories;
        }

        public async Task<ThoughtCategory> InsertAsync(ThoughtCategory newItem)
        {
            await _repository.InsertAsync(newItem);
            return newItem;
        }
    }
}