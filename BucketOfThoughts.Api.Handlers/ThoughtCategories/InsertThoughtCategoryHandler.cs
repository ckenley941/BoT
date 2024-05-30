using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Data;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class InsertThoughtCategoryHandler 
    {
        protected readonly ThoughtCategoriesService _service;
        public InsertThoughtCategoryHandler(ThoughtCategoriesService service)
        {
            _service = service;
        }

        public async Task<ThoughtCategory> HandleAsync(ThoughtCategory newItem)
        {
            return await _service.InsertAsync(newItem);
        }
    }
}
