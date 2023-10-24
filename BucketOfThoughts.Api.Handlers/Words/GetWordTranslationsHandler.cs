using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;

namespace BucketOfThoughts.Api.Handlers.Words
{
    public class GetWordTranslationsHandler
    {
        protected readonly WordsService _service;
        public GetWordTranslationsHandler(WordsService service)
        {
            _service = service;
        }
        public async Task<List<WordDto>> HandleAsync(int id)
        {
            return await _service.GetTranslationsAsync(id);
        }
    }
}
