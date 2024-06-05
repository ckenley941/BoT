using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Outdoors
{
    public class GetOutdoorActivityLogsHandler
    {
        protected readonly OutdoorActivityLogService _service;
        public GetOutdoorActivityLogsHandler(OutdoorActivityLogService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<OutdoorActivityLogDto>> HandleAsync()
        {
            return await _service.GetDtoAsync();
        }
    }
}
