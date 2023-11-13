using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtsRepository : BaseCrudRepositoryEF<Thought>
    {
        public ThoughtsRepository(ThoughtsDbContext context) : base(context) { }

        public override async Task<Thought> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.ThoughtCategory).Include(x => x.ThoughtDetails).SingleOrDefaultAsync(x => x.ThoughtId == id);
        }
    }
}
