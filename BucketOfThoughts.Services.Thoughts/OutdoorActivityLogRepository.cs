using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;

namespace BucketOfThoughts.Services.Thoughts
{
    public interface IOutdoorActivityLogRepository : ICrudRepository<OutdoorActivityLog>
    {
    }

    public class OutdoorActivityLogRepository : BaseCrudRepositoryEF<OutdoorActivityLog>, IOutdoorActivityLogRepository
    {
        public OutdoorActivityLogRepository(ThoughtsDbContext context) : base(context) { }
    }
}