using BucketOfThoughts.Services.ThoughtModule.Data;
using BucketOfThoughts.Services.ThoughtModule;

namespace BucketOfThoughts.Api.ThoughtHandlers
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
