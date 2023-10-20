using BucketOfThoughts.Services.Thoughts;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public abstract class BaseWordHandler
    {
        protected readonly ThoughtsService _service;
        public BaseWordHandler(ThoughtsService service)
        {
            _service = service;
        }
    }
}
