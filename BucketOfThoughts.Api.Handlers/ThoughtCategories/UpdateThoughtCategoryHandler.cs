using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.ThoughtCategories
{
    public class UpdateThoughtCategoryHandler
    {
        protected readonly ThoughtCategoriesService _service;
        public UpdateThoughtCategoryHandler(ThoughtCategoriesService service)
        {
            _service = service;
        }

        public async Task HandleAsync(ThoughtCategoryDto updateItem)
        {
            await _service.UpdateAsync(updateItem);
        }
    }
}
