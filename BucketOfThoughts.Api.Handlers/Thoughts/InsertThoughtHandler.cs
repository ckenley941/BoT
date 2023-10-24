using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class InsertThoughtHandler 
    {
        protected readonly ThoughtsService _service;
        public InsertThoughtHandler(ThoughtsService service)
        {
            _service = service;
        }
        public async Task<Thought> HandleAsync(InsertThoughtDto newItem)
        {
            return await _service.InsertAsync(newItem);
        }
    }
}
