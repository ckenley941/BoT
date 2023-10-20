using BucketOfThoughts.Api.ThoughtHandlers.Thoughts;

namespace BucketOfThoughts.Api.ThoughtModule.Handlers
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
