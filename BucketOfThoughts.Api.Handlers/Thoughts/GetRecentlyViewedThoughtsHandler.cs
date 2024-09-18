using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetRecentlyViewedThoughtsHandler
    {
        protected readonly IThoughtsService _service;
        public GetRecentlyViewedThoughtsHandler(IThoughtsService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<ThoughtGridDto>> HandleAsync()
        {
            return await _service.GetRecentlyViewedThoughtsAsync();
        }
    }
}
