using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Api.Handlers.Thoughts;

namespace BucketOfThoughts.Api.Handlers.ThoughtCategories
{
    public class GetThoughtCategoriesHandler 
    {
        protected readonly ThoughtCategoriesService _service;
        public GetThoughtCategoriesHandler(ThoughtCategoriesService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<ThoughtCategory>> HandleAsync()
        {
            return await _service.GetAsync();
        }
    }
}
