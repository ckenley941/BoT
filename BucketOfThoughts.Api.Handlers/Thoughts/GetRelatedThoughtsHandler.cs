using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetRelatedThoughtsHandler
    {
        protected readonly IThoughtsService _service;
        public GetRelatedThoughtsHandler(IThoughtsService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<ThoughtGridDto>> HandleAsync(int id)
        {
            return await _service.GetRelatedThoughtsGridAsync(id);
        }
    }
}
