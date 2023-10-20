using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetRandomThoughtHandler : BaseWordHandler
    {
        public GetRandomThoughtHandler(ThoughtsService service) : base(service) { }
        public async Task<Thought> HandleAsync()
        {
            //return new Thought() { Description = "So random1" };
            return await _service.GetRandomThoughtAsync();
        }
    }
}
