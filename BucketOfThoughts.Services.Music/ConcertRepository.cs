using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Music.Data;

namespace BucketOfThoughts.Services.Music
{
    public interface IConcertRepository : ICrudRepository<Concert>
    {
    }

    public class ConcertRepository : BaseCrudRepositoryEF<Concert>, IConcertRepository
    {
        public ConcertRepository(MusicDbContext context) : base(context) { }

    }
}