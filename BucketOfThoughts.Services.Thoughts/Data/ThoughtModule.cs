using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class ThoughtModule : BaseDbTable
{
    public string Description { get; set; } = null!;
    public virtual ICollection<ThoughtBucket> ThoughtBuckets { get; set; } = new List<ThoughtBucket>();
}
