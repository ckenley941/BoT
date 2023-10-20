using BucketOfThoughts.Services.ThoughtModule.Data;
using BucketOfThoughts.Services.ThoughtModule;

namespace BucketOfThoughts.Api.ThoughtHandlers
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
