using System;
using System.Collections.Generic;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class ThoughtWebsiteLink
{
    public int ThoughtWebsiteLinkId { get; set; }

    public int ThoughtId { get; set; }

    public int WebsiteLinkId { get; set; }

    public virtual Thought Thought { get; set; } = null!;

    public virtual WebsiteLink WebsiteLink { get; set; } = null!;
}
