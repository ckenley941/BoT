using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetWebsiteLinksHandler
    {
        protected readonly IThoughtsService _service;
        public GetWebsiteLinksHandler(IThoughtsService service)
        {
            _service = service;
        }
        public async Task<ThoughtDto> HandleAsync(int? thoughtBucketId)
        {
            return await _service.GetRandomThoughtAsync(thoughtBucketId, false);
        }
    }
}
