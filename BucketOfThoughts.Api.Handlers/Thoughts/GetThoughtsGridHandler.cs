using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetThoughtsGridHandler
    {
        protected readonly IThoughtsService _service;
        public GetThoughtsGridHandler(IThoughtsService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<ThoughtGridDto>> HandleAsync()
        {
            return await _service.GetAsync();
        }
    }
}
