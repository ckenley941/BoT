using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetThoughtsHandler
    {
        protected readonly ThoughtsService _service;
        public GetThoughtsHandler(ThoughtsService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<GetThoughtDto>> HandleAsync()
        {
            return await _service.GetAsync();
        }
    }
}
