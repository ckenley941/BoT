using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class ThoughtDetail : BaseModifiableDbTable
{
    public string Description { get; set; } = null!;
    public int ThoughtId { get; set; }

    public int? SortOrder { get; set; }

    public virtual Thought Thought { get; set; } = null!;
}
