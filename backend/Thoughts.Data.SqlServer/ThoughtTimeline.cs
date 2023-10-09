﻿using System;
using System.Collections.Generic;

namespace Thoughts.Data.SqlServer;

public partial class ThoughtTimeline
{
    public int ThoughtTimelineId { get; set; }

    public int ThoughtId { get; set; }

    public int TimelineId { get; set; }

    public virtual Thought Thought { get; set; } = null!;

    public virtual Timeline Timeline { get; set; } = null!;
}
