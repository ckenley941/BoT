using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class OutdoorActivityLog : BaseModifiableDbTable
{
    public string ActivityName { get; set; } = null!;  
    public string ActivityType { get; set; } = null!;
    public DateOnly ActivityDate { get; set; }
    public string? GeographicArea { get; set; }
    public double? ActivityLength { get; set; }
    public double? ElevationGain { get; set; }
    public TimeSpan? ActivityTime { get; set; }
    public string? ActivityUrl { get; set; }
}
