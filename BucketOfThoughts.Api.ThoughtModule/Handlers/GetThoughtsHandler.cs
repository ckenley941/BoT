using BucketOfThoughts.Api.ThoughtHandlers.Data;
using BucketOfThoughts.Api.ThoughtHandlers.Thoughts;

namespace BucketOfThoughts.Api.ThoughtModule.Handlers
{
    public class GetThoughtsHandler : BaseThoughtHandler
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
