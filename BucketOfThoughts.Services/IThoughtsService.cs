using Thoughts.Data.SqlServer;
using Common.Data.Objects.Thoughts;

namespace BucketOfThoughts.Services
{
    public interface IThoughtsService
    {
        public Task<Thought> GetRandomThoughtAsync();
        public Task<List<ThoughtCategory>> GetThoughtCategoriesAsync();
        public Task<Thought> AddThoughtAsync(InsertThoughtDto newThought);
    }
}
