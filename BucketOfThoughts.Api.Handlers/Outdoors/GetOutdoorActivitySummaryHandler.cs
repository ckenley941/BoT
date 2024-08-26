using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Outdoors
{
    public class GetOutdoorActivitySummaryHandler
    {
        protected readonly OutdoorActivityLogService _service;
        public GetOutdoorActivitySummaryHandler(OutdoorActivityLogService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<OutdoorActivitySummaryDto>> HandleAsync(DateOnly dateFrom, DateOnly dateTo, List<string> activityTypes)
        {
            return await _service.GetOutdoorActivitySummaryAsync(dateFrom, dateTo, activityTypes);
        }
    }
}
