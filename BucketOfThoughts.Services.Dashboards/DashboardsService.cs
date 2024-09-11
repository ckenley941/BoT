using BucketOfThoughts.Core.Infrastructure.Extensions;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Service.Dashboards.Objects;
using BucketOfThoughts.Services.Music.Data;
using System.Text.Json.Nodes;

namespace BucketOfThoughts.Service.Dashboards
{
    public class DashboardsService
    {
        private readonly DashboardsServiceContainer _serviceContainer;
        public DashboardsService(DashboardsServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public async Task<DashboardResponse> Get(string dashboardType, int? thoughtBucketId)
        {
            var dashboardResults = new DashboardResponse();
            
            switch (dashboardType)
            {
                case "RandomThought":
                    dashboardResults.Data = (await _serviceContainer.ThoughtsService.GetRandomThoughtAsync(thoughtBucketId)).AsEnumerable();
                    break;
                case "RandomWord":
                    dashboardResults.Data = (await _serviceContainer.WordsService.GetRandomWordAsync()).AsEnumerable();
                    break;
                case "RandomOutdoorActivity":
                    dashboardResults.Data = (await _serviceContainer.OutdoorActivityLogService.GetRandomDtoAsync()).AsEnumerable();
                    break;
                case "RecentThought":
                    dashboardResults.Data = (await _serviceContainer.ThoughtsService.GetRecentThoughtAsync()).AsEnumerable();
                    break;
                case "RandomConcert":
                    var a = await _serviceContainer.ConcertService.GetRandomDtoAsync(new GetQueryParams<Concert>() { IncludeProperties = "Band,Venue,Songs" });
                    var b = System.Text.Json.JsonSerializer.Serialize(a);
                    dashboardResults.Data = (await _serviceContainer.ConcertService.GetRandomDtoAsync(new GetQueryParams<Concert>(){ IncludeProperties = "Band,Venue,Songs" }))
                        .AsEnumerable();
                    break;
                case "AllThoughts":
                    dashboardResults.Data = await _serviceContainer.ThoughtsService.GetGridAsync();
                    break;
            }

            return dashboardResults;
        }
    }
}