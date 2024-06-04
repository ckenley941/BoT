using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.ThoughtBuckets
{
    public class UpdateThoughtBucketHandler
    {
        protected readonly ThoughtBucketsService _service;
        public UpdateThoughtBucketHandler(ThoughtBucketsService service)
        {
            _service = service;
        }

        public async Task HandleAsync(ThoughtBucketDto updateItem)
        {
            await _service.UpdateAsync(updateItem);
        }
    }
}
