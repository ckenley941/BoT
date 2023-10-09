using Common.Data.Objects.Words;
using EnsenaMe.Data.Entities;

namespace BucketOfThoughts.Api.Services
{
    public interface IWordsService
    {
        public Task<int> AddOrUpdateWordCardAsync(InsertWordCardDto insertedWord);
        public Task<Word> AddWordIfNotExistsAsync(string primaryWord, int languageId);
        public Task<bool> AddPronunicationIfNotExistsAsync(int wordId, List<InsertWordPronunciation> pronunciation);
        public Task<WordTranslationDto> GetByIdAsync(int id);
        public Task<WordTranslationDto> GetByWord(string description);
        public Task<WordTranslationDto> GetRandomWordAsync();
        public Task<List<WordDto>> GetTranslationsAsync(int id);
        public Task<List<WordContextDto>> GetTranslationsWithContextAsync(int id);
        public Task<List<WordRelationshipDto>> GetWordRelationshipsAsync(int id);

    }
}
