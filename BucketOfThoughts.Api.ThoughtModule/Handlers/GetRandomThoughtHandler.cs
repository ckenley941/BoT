using BucketOfThoughts.Api.ThoughtHandlers.Data;
using BucketOfThoughts.Api.ThoughtHandlers.Thoughts;

namespace BucketOfThoughts.Api.ThoughtModule.Handlers
{
    public class GetRandomThoughtHandler : BaseThoughtHandler
    {
        public GetRandomThoughtHandler(ThoughtsService service) : base(service) { }
        public async Task<Thought> HandleAsync()
        {
            //return new Thought() { Description = "So random1" };
            return await _service.GetRandomThoughtAsync();
        }
    }
}
