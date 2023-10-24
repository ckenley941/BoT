using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;

namespace BucketOfThoughts.Api.Handlers.Words
{
    public class GetWordsHandler 
    {
        protected readonly WordsService _service;
        public GetWordsHandler(WordsService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<GetWordDto>> HandleAsync()
        {
            return await _service.GetAsync();
        }
    }
}
