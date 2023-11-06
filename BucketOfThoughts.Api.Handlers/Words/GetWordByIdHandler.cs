using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;

namespace BucketOfThoughts.Api.Handlers.Words
{
    public class GetWordByIdHandler
    {
        protected readonly WordsService _service;
        public GetWordByIdHandler(WordsService service)
        {
            _service = service;
        }
        public async Task<WordTranslationDto> HandleAsync(int id)
        {
            return await _service.GetByIdAsync(id);
        }
    }
}
