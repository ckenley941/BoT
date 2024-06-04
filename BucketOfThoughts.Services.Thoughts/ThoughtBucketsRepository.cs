using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;

namespace BucketOfThoughts.Services.Thoughts
{
    public interface IThoughtBucketsRepository : ICrudRepository<ThoughtBucket>
    {
    }

    public class ThoughtBucketsRepository : BaseCrudRepositoryEF<ThoughtBucket>, IThoughtBucketsRepository
    {
        public ThoughtBucketsRepository(ThoughtsDbContext context) : base(context) { }
    }
}