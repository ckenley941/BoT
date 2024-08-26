using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Objects;

public class OutdoorActivitySummaryDto : BaseDto
{
    public string ActivityType { get; set; } = null!;
    public double? TotalActivityLength { get; set; }
    public double? TotalElevationGain { get; set; }
}
