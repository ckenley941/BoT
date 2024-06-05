using BucketOfThoughts.Core.Infrastructure.Enums;

namespace BucketOfThoughts.Api.Handlers.Outdoors
{
    public class GetOutdoorActivityTypesHandler
    {
        public IEnumerable<string> Handle()
        {
            return Enum.GetValues(typeof(OutdoorActivityTypes)).Cast<OutdoorActivityTypes>().Select(t => t.ToString()).ToList();
        }
    }
}
