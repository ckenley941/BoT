using System;
using System.Collections.Generic;

namespace BucketOfThoughts.Api.ThoughtHandlers.Data;

public partial class ThoughtAdditionalInfo
{
    public int ThoughtAdditionalInfoId { get; set; }

    public int ThoughtDetailId { get; set; }

    public int ThoughtCategoryAdditionalInfoId { get; set; }

    public string? Value { get; set; }

    public int RowId { get; set; }

    public virtual ThoughtCategoryAdditionalInfo ThoughtCategoryAdditionalInfo { get; set; } = null!;

    public virtual ThoughtDetail ThoughtDetail { get; set; } = null!;
}
