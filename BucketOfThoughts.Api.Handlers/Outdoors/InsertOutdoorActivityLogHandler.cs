using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Outdoors
{
    public class InsertOutdoorActivityLogHandler
    {

        protected readonly OutdoorActivityLogService _service;
        public InsertOutdoorActivityLogHandler(OutdoorActivityLogService service)
        {
            _service = service;
        }

        public async Task<OutdoorActivityLog> HandleAsync(OutdoorActivityLogDto newItem)
        {
            return await _service.InsertAsync(newItem);
        }
    }
}
