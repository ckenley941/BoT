using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class WebsiteLink : BaseModifiableDbTable
{
    public string WebsiteUrl { get; set; } = null!;

    public string? WebsiteDesc { get; set; }

    public int? SortOrder { get; set; }

    public virtual ICollection<ThoughtWebsiteLink> ThoughtWebsiteLinks { get; set; } = new List<ThoughtWebsiteLink>();
}
