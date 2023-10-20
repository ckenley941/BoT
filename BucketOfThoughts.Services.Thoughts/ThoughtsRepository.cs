using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Services.Thoughts.Data;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtsRepository : BaseCrudRepositoryEF<Thought>
    {
        public ThoughtsRepository(ThoughtsDbContext context) : base(context) { }
    }
}
