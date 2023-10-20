using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Thoughts;

namespace BucketOfThoughts.Api.Handlers.Words
{
    public abstract class BaseWordHandler
    {
        protected readonly WordsService _service;
        public BaseWordHandler(WordsService service)
        {
            _service = service;
        }
    }
}
