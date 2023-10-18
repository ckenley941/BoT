using System;
using System.Collections.Generic;

namespace Thoughts.Data.SqlServer;

public partial class WebsiteLink
{
    public int WebsiteLinkId { get; set; }

    public Guid WebsiteLinkGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public string WebsiteUrl { get; set; } = null!;

    public string? WebsiteDesc { get; set; }

    public int? SortOrder { get; set; }

    public virtual ICollection<ThoughtWebsiteLink> ThoughtWebsiteLinks { get; } = new List<ThoughtWebsiteLink>();
}
