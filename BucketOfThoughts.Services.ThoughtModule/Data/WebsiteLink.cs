using System;
using System.Collections.Generic;

namespace BucketOfThoughts.Services.ThoughtModule.Data;

public partial class WebsiteLink
{
    public int WebsiteLinkId { get; set; }

    public Guid WebsiteLinkGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public string WebsiteUrl { get; set; } = null!;

    public string? WebsiteDesc { get; set; }

    public int? SortOrder { get; set; }

    public virtual ICollection<ThoughtWebsiteLink> ThoughtWebsiteLinks { get; set; } = new List<ThoughtWebsiteLink>();
}
