using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class AddThoughtHandler : BaseWordHandler
    {
        public AddThoughtHandler(ThoughtsService service) : base(service) { }
        public async Task<Thought> HandleAsync(InsertThoughtDto insertThought)
        {
            return await _service.AddThoughtAsync(insertThought);
        }
    }
}
