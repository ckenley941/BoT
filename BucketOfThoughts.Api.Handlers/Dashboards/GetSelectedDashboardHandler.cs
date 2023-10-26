using BucketOfThoughts.Service.Dashboards;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Service.Dashboards.Objects;

namespace BucketOfThoughts.Api.Handlers.Dashboards
{
    public class GetSelectedDashboardHandler
    {
        private readonly DashboardsService _service;
        public GetSelectedDashboardHandler(DashboardsService service)
        {
            _service = service;
        }
        public async Task<DashboardResponse> HandleAsync(string dashboardType)
        {
            return await _service.Get(dashboardType);
        }
    }
}
