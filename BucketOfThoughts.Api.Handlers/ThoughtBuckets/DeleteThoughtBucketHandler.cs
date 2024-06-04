using BucketOfThoughts.Services.Thoughts;

namespace BucketOfThoughts.Api.Handlers.ThoughtBuckets
{
    public class DeleteThoughtBucketHandler
    {
        protected readonly ThoughtBucketsService _service;
        public DeleteThoughtBucketHandler(ThoughtBucketsService service)
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
