using System;
using System.Collections.Generic;

namespace Thoughts.Data.SqlServer;

public partial class Thought
{
    public int ThoughtId { get; set; }

    public Guid ThoughtGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public string Description { get; set; } = null!;

    public int ThoughtCategoryId { get; set; }

    public bool IsQuote { get; set; }

    public virtual ThoughtCategory ThoughtCategory { get; set; } = null!;

    public virtual ICollection<ThoughtDetail> ThoughtDetails { get; } = new List<ThoughtDetail>();

    public virtual ICollection<ThoughtTimeline> ThoughtTimelines { get; } = new List<ThoughtTimeline>();

    public virtual ICollection<ThoughtWebsiteLink> ThoughtWebsiteLinks { get; } = new List<ThoughtWebsiteLink>();
}
