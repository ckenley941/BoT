using BucketOfThoughts.Services.ThoughtModule;

namespace BucketOfThoughts.Api.ThoughtHandlers
{
    public abstract class BaseThoughtHandler
    {
        protected readonly ThoughtsService _service;
        public BaseThoughtHandler(ThoughtsService service)
        {
            _service = service;
        }
    }
}
