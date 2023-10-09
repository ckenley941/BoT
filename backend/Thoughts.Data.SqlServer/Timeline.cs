using System;
using System.Collections.Generic;

namespace Thoughts.Data.SqlServer;

public partial class Timeline
{
    public int TimelineId { get; set; }

    public Guid TimelineGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public virtual ICollection<ThoughtTimeline> ThoughtTimelines { get; } = new List<ThoughtTimeline>();
}
