using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Thoughts
{
    public class OutdoorActivityLogService : BaseService<OutdoorActivityLog, OutdoorActivityLogDto>
    {
        protected readonly ThoughtsDbContext _dbContext;
        public OutdoorActivityLogService(ThoughtsDbContext dbContext, IDistributedCache cache, IMapper mapper) : base(dbContext, cache, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OutdoorActivityLogDto>> GetDtoAsync()
        {
            var outdoorActivityLogs = await _dbContext.OutdoorActivityLogs
                .OrderByDescending(ota => ota.ActivityDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OutdoorActivityLogDto>>(outdoorActivityLogs);
        }

        public async Task<OutdoorActivityLogDto> GetRandomOutdoorActivityAsync()
        {
            var outdoorActivities = await _dbContext.OutdoorActivityLogs.ToListAsync();

            if (outdoorActivities?.Count <= 0)
            {
                throw new Exception("Outdoor activities not found"); //TODO don't throw error?
            }

            var rand = new Random();

            var randOutdoorActivity = outdoorActivities[rand.Next(outdoorActivities.Count)];

            return _mapper.Map<OutdoorActivityLogDto>(randOutdoorActivity);
        }

        public async Task<IEnumerable<OutdoorActivitySummaryDto>> GetOutdoorActivitySummaryAsync(DateOnly dateFrom, DateOnly dateTo, List<string> activityTypes)
        {
            throw new NotImplementedException();
            //var outdoorActivities = (await _repository.GetAsync()).Where(oa => oa.ActivityDate >= dateFrom && oa.ActivityDate <= dateTo);

            //if (activityTypes?.Count > 0)
            //{
            //    outdoorActivities = outdoorActivities.Where(oa => activityTypes.Contains(oa.ActivityType));
            //}

            //var outdoorActivitySummary = outdoorActivities.GroupBy(oa => oa.ActivityType)
            //    .Select(oa =>            
            //    new OutdoorActivitySummaryDto()
            //    {
            //        ActivityType = oa.Key,
            //        TotalActivityLength = oa.Sum(o => o.ActivityLength),
            //        TotalElevationGain = oa.Sum(o => o.ElevationGain)
            //    }
            //).ToList();


            //return outdoorActivitySummary;
        }
    }
}
