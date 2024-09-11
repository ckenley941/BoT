using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetThoughtBankHandler
    {
        protected readonly IThoughtsService _service;
        public GetThoughtBankHandler(IThoughtsService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<ThoughtGridDto>> HandleAsync()
        {
            return await _service.GetThoughtBankAsync();
        }
    }
}
