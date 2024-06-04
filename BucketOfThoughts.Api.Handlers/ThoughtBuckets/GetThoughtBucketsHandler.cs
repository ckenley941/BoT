using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.ThoughtBuckets
{
    public class GetThoughtBucketsHandler 
    {
        protected readonly ThoughtBucketsService _service;
        public GetThoughtBucketsHandler(ThoughtBucketsService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<ThoughtBucketDto>> HandleAsync()
        {
            return await _service.GetAsync();
        }
    }
}
