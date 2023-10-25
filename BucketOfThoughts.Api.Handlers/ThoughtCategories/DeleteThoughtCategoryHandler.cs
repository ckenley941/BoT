using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Api.Handlers.Thoughts;

namespace BucketOfThoughts.Api.Handlers.ThoughtCategories
{
    public class DeleteThoughtCategoryHandler
    {
        protected readonly ThoughtCategoriesService _service;
        public DeleteThoughtCategoryHandler(ThoughtCategoriesService service)
        {
            _service = service;
        }

        public async Task HandleAsync(ThoughtCategory deleteItem)
        {
            await _service.DeleteAsync(deleteItem);
        }
    }
}
