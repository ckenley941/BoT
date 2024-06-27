using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.EntityFrameworkCore;

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