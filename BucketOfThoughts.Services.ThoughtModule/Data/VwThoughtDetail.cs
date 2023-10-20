﻿using System;
using System.Collections.Generic;

namespace BucketOfThoughts.Services.ThoughtModule.Data;

public partial class VwThoughtDetail
{
    public int ThoughtId { get; set; }

    public string ThoughtDesc { get; set; } = null!;

    public string ThoughtCategoryDesc { get; set; } = null!;

    public string? ThoughtDetailDesc { get; set; }

    public string? ThoughtAdditionalInfo { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }
}
