using Common.Data.Objects.Words;
using BucketOfThoughts.Api.Services;
using EnsenaMe.Data.MongoDB;
using EnsenaMe.Data.MongoDB;
using Thoughts.Data.SqlServer;
using Common.Data.Objects.Thoughts;

namespace BucketOfThoughts.Api.Handlers
{
    public class ThoughtsHandler
    {
        private readonly IThoughtsService _thoughtsService;

        public ThoughtsHandler(IThoughtsService thoughtsService)
        {
            _thoughtsService = thoughtsService;
        }
        public async Task<Thought> GetRandomThoughtAsync()
        {
            var thought = await _thoughtsService.GetRandomThoughtAsync();
            return thought;
        }
        public async Task<List<ThoughtCategory>> GetThoughtCategoriesAsync()
        {
            var thoughtCategories = await _thoughtsService.GetThoughtCategoriesAsync();
            return thoughtCategories;
        }
        public async Task<Thought> AddThoughtAsync(InsertThoughtDto newThought)
        {
            var thought = await _thoughtsService.AddThoughtAsync(newThought);
            return thought;
        }

    }
}
