using BucketOfThoughts.Core.Infrastructure.Enums;

namespace BucketOfThoughts.Api.Handlers.Outdoors
{
    public class GetOutdoorActivitiesHandler
    {
        public IEnumerable<string> Handle()
        {
            return Enum.GetValues(typeof(OutdoorActivities)).Cast<OutdoorActivities>().Select(t => t.ToString()).ToList();
        }
    }
}
