using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class InsertThoughtHandler 
    {
        protected readonly IThoughtsService _service;
        public InsertThoughtHandler(IThoughtsService service)
        {
            _service = service;
        }
        public async Task<Thought> HandleAsync(InsertThoughtDto newItem)
        {
            return await _service.InsertAsync(newItem);
        }
    }
}
