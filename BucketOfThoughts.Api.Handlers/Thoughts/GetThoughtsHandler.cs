using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetThoughtsHandler : BaseWordHandler
    {
        public GetThoughtsHandler(ThoughtsService service) : base(service) { }
        public async Task<IEnumerable<Thought>> HandleAsync()
        {
            //var thoughts = new List<Thought>();
            //thoughts.Add(new Thought() { Description = "Test new Module" });
            //thoughts.Add(new Thought() { Description = "Test new Module2" } );
            //return thoughts.AsEnumerable();
            return await _service.GetThoughtsAsync();
        }
    }
}
