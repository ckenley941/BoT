using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;

namespace BucketOfThoughts.Services.Thoughts
{
    public interface IThoughtCategoriesRepository : ICrudRepository<ThoughtCategory>
    {
    }

    public class ThoughtCategoriesRepository : BaseCrudRepositoryEF<ThoughtCategory>, IThoughtCategoriesRepository
    {
        public ThoughtCategoriesRepository(ThoughtsDbContext context) : base(context) { }
    }
}