using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Objects
{
    public class OutdoorActivityLogDto : BaseDto
    {
        public string ActivityName { get; set; } = null!;
        public string ActivityType { get; set; } = null!;
        public DateOnly ActivityDate { get; set; }
        public string? GeographicArea { get; set; }
        public double? ActivityLength { get; set; }
        public double? ElevationGain { get; set; }
        public int? ActivityTimeHours { get; set; }
        public int? ActivityTimeMinutes { get; set; }
        public string? ActivityUrl { get; set; }
    }
}
