using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class Thought : BaseModifiableDbTable
{
    public Guid ThoughtGuid { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = null!;
    public int ThoughtCategoryId { get; set; }
    public string TextType { get; set; } = "PlainText";
    public virtual ICollection<RelatedThought> RelatedThoughtThoughtId1Navigations { get; set; } = new List<RelatedThought>();

    public virtual ICollection<RelatedThought> RelatedThoughtThoughtId2Navigations { get; set; } = new List<RelatedThought>();

    public virtual ThoughtCategory ThoughtCategory { get; set; } = null!;

    public virtual ICollection<ThoughtDetail> ThoughtDetails { get; set; } = new List<ThoughtDetail>();

    public virtual ICollection<ThoughtWebsiteLink> ThoughtWebsiteLinks { get; set; } = new List<ThoughtWebsiteLink>();
}
