using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;

namespace BucketOfThoughts.Api.Handlers.Words
{
    public class GetRandomWordHandler : BaseWordHandler
    {
        public GetRandomWordHandler(WordsService service) : base(service) { }
        public async Task<WordTranslationDto> HandleAsync()
        {
            return await _service.GetRandomWordAsync();
        }
    }
}
