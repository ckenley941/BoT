using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Services.Thoughts
{
    public interface IThoughtsRepository: ICrudRepository<Thought>
    {
        IEnumerable<Thought>? GetRelatedThoughts(int thoughtId);
    }

    public class ThoughtsRepository : BaseCrudRepositoryEF<Thought>, IThoughtsRepository
    {

        protected readonly new ThoughtsDbContext _context;
        public ThoughtsRepository(ThoughtsDbContext context) : base(context) 
        {
            _context = context;
        }

        public override async Task<Thought> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.ThoughtCategory).Include(x => x.ThoughtDetails).SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Thought>? GetRelatedThoughts(int thoughtId)
        {
            var relatedThought1 = _context.RelatedThoughts.Where(x => x.ThoughtId1 == thoughtId)
                .Join(_context.Thoughts.Include(x => x.ThoughtDetails).Include(x => x.ThoughtCategory), rt => rt.ThoughtId2, t => t.Id, (rt, t) => t);

            var relatedThought2 = _context.RelatedThoughts.Where(x => x.ThoughtId2 == thoughtId)
                .Join(_context.Thoughts.Include(x => x.ThoughtDetails).Include(x => x.ThoughtCategory), rt => rt.ThoughtId1, t => t.Id, (rt, t) => t);

            return relatedThought1.Union(relatedThought2).AsEnumerable();
        }
    }
}
