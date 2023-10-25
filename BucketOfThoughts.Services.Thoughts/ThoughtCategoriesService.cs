using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.Extensions.Caching.Distributed;
using BucketOfThoughts.Services.Thoughts.Objects;
using BucketOfThoughts.Core.Infrastructure.Extensions;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtCategoriesService : BaseService<ThoughtCategory>
    {
        public ThoughtCategoriesService(ICrudRepository<ThoughtCategory> repository, IDistributedCache cache) : base (repository, cache)
        {
        }

        public async Task<IEnumerable<ThoughtCategory>> GetAsync()
        {
            var thougtCategories = await base.GetFromCacheAsync("ThoughtCategories");
            return thougtCategories;
        }
    }
}