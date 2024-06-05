using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class ThoughtBucket : BaseModifiableDbTable
{
    public int ThoughtModuleId { get; set; }
    public virtual ThoughtModule ThoughtModule { get; set; } = null!;
    public string Description { get; set; } = null!;

    public int? ParentId { get; set; }

    public int? SortOrder { get; set; }
    public bool? ShowOnDashboard { get; set; } = true;
    public virtual ICollection<Thought> Thoughts { get; set; } = new List<Thought>();
}
