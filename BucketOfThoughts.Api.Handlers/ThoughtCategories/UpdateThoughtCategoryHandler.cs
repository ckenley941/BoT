using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Api.Handlers.Thoughts;

namespace BucketOfThoughts.Api.Handlers.ThoughtCategories
{
    public class UpdateThoughtCategoryHandler
    {
        protected readonly ThoughtCategoriesService _service;
        public UpdateThoughtCategoryHandler(ThoughtCategoriesService service)
        {
            _service = service;
        }

        public async Task HandleAsync(ThoughtCategory updateItem)
        {
            await _service.UpdateAsync(updateItem);
        }
    }
}
