using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Services.ThoughtModule.Data;

namespace BucketOfThoughts.Services.ThoughtModule
{
    public class ThoughtsRepository : BaseCrudRepositoryEF<Thought>
    {
        public ThoughtsRepository(BucketOfThoughtsContext context) : base(context) { }
    }
}
