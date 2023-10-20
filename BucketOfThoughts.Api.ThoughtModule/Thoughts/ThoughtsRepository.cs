using BucketOfThoughts.Api.ThoughtHandlers.Data;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Api.ThoughtHandlers.Thoughts
{
    public class ThoughtsRepository : BaseCrudRepositoryEF<Thought>
    {
        public ThoughtsRepository(BucketOfThoughtsContext context) : base(context){}
    }
}
