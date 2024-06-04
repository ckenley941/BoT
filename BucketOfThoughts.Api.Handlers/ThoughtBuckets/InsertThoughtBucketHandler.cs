using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Data;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class InsertThoughtBucketHandler 
    {
        protected readonly ThoughtBucketsService _service;
        public InsertThoughtBucketHandler(ThoughtBucketsService service)
        {
            _service = service;
        }

        public async Task<ThoughtBucket> HandleAsync(ThoughtBucket newItem)
        {
            return await _service.InsertAsync(newItem);
        }
    }
}
