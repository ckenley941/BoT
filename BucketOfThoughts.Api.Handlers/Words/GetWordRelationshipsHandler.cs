using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;

namespace BucketOfThoughts.Api.Handlers.Words
{
    public class GetWordRelationshipsHandler
    {
        protected readonly WordsService _service;
        public GetWordRelationshipsHandler(WordsService service)
        {
            _service = service;
        }
        public async Task<List<WordRelationshipDto>> HandleAsync(int id)
        {
            return await _service.GetWordRelationshipsAsync(id);
        }
    }
}
