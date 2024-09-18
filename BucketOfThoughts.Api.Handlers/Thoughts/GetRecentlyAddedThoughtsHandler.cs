using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetRecentlyAddedThoughtsHandler
    {
        protected readonly IThoughtsService _service;
        public GetRecentlyAddedThoughtsHandler(IThoughtsService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<ThoughtGridDto>> HandleAsync()
        {
            return await _service.GetRecentlyAddedThoughtsAsync();
        }
    }
}
