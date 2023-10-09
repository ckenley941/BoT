using Common.Data.Objects.Words;
using BucketOfThoughts.Api.Services;
using EnsenaMe.Data.MongoDB;
using EnsenaMe.Data.MongoDB;

namespace BucketOfThoughts.Api.Handlers
{
    public class RandomWordHandler
    {
        private readonly IWordsService _wordsService;
        private readonly MongoDBService _dbService;

        public RandomWordHandler(IWordsService wordsService, MongoDBService dbService)
        {
            _wordsService = wordsService;
            _dbService = dbService;
        }
        public async Task<WordTranslationDto> GetAsync()
        {
            //TODO - create new service that queries NoSQL DB
            // NoSQL DB is a copy of the record from SQL DB with Word guid as key
            //And the rest of the WordTranslationDto as the json
            //Service copies over to NoSQL DB whenever word added/updated/deleted
            //It turns the saved object into a WordTranslationDto, serializes it, and writes to NoSQL DB
            //Need to query for any missing pieces so its always a complete WordTranslationDto object
            //This should all happen in a background task after Add/Update so it doesn't slow down User experience

            //TODO longer term keep cache of words when pulling random word 
            //MongoDBConnection.ConnectToDb(); 


            WordTranslationDto word = await _wordsService.GetRandomWordAsync();
            await _dbService.AddWordAsync(word);
            return word;
        }
    }
}
