﻿using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class RelatedThought : BaseDbTable
{
    public int ThoughtId1 { get; set; }

    public int ThoughtId2 { get; set; }

    public virtual Thought ThoughtId1Navigation { get; set; } = null!;

    public virtual Thought ThoughtId2Navigation { get; set; } = null!;
}
