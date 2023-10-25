﻿using System;
using System.Collections.Generic;

namespace BucketOfThoughts.Services.Thoughts.Data;

public partial class Thought
{
    public int ThoughtId { get; set; }

    public Guid ThoughtGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public string Description { get; set; } = null!;

    public int ThoughtCategoryId { get; set; }

    public bool IsQuote { get; set; }

    public virtual ThoughtCategory ThoughtCategory { get; set; } = null!;

    public virtual ICollection<ThoughtDetail> ThoughtDetails { get; set; } = new List<ThoughtDetail>();
}
