using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;

namespace BucketOfThoughts.Api.Handlers.Words
{
    public class GetWordTranslationsWithContextHandler
    {
        protected readonly WordsService _service;
        public GetWordTranslationsWithContextHandler(WordsService service)
        {
            _service = service;
        }
        public async Task<List<WordContextDto>> HandleAsync(int id)
        {
            return await _service.GetTranslationsWithContextAsync(id);
        }
    }
}
