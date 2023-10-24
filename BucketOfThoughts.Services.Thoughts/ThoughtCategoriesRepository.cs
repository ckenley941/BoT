using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Services.Thoughts.Data;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtCategoriesRepository : BaseCrudRepositoryEF<ThoughtCategory>
    {
        public ThoughtCategoriesRepository(ThoughtsDbContext context) : base(context) { }
    }
}