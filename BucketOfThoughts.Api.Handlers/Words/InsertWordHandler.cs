using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;

namespace BucketOfThoughts.Api.Handlers.Words
{
    public class InsertWordHandler
    {
        protected readonly WordsService _service;
        public InsertWordHandler(WordsService service)
        {
            _service = service;
        }
        public async Task<int> HandleAsync(InsertWordCardDto newItem)
        {
            return await _service.InsertOrUpdateWordCardAsync(newItem);
        }
    }
}
