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

        public async Task HandleAsync(int id)
        {
            //Eventually add logic that first checks to see if any Thoughts are tied to the category
                //If no - we're good
                //If yes - either auto move everything to ParentId if null to Other
                //OR give them a dropdown to choose the new category defaulting to select value based on logic above
            await _service.DeleteAsync(id);
        }
    }
}
