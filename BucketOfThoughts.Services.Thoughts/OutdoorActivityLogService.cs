using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Thoughts
{
    public class OutdoorActivityLogService : BaseService<OutdoorActivityLog, OutdoorActivityLogDto>
    {
        public OutdoorActivityLogService(IOutdoorActivityLogRepository repository, IDistributedCache cache, IMapper mapper) : base(repository, cache, mapper)
        {
        }

        public async Task<IEnumerable<OutdoorActivityLogDto>> GetDtoAsync()
        {
            var queryParams = new GetQueryParams<OutdoorActivityLog>()
            {
                 OrderBy =  t => t.OrderByDescending(ota => ota.ActivityDate)
            };

            var outdoorActivityLogs = await _repository.GetAsync(queryParams);
            return _mapper.Map<IEnumerable<OutdoorActivityLogDto>>(outdoorActivityLogs);
        }

        public async Task<OutdoorActivityLogDto> GetRandomOutdoorActivityAsync()
        {
            var outdoorActivities = (await _repository.GetAsync()).ToList();

            if (outdoorActivities?.Count <= 0)
            {
                throw new Exception("Outdoor activities not found"); //TODO don't throw error?
            }

            var rand = new Random();

            var randOutdoorActivity = outdoorActivities[rand.Next(outdoorActivities.Count)];

            return _mapper.Map<OutdoorActivityLogDto>(randOutdoorActivity);
        }
    }
}
