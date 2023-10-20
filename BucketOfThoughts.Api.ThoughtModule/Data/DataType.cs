using System;
using System.Collections.Generic;

namespace BucketOfThoughts.Api.ThoughtHandlers.Data;

public partial class DataType
{
    public int DataTypeId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<ThoughtCategoryAdditionalInfo> ThoughtCategoryAdditionalInfos { get; set; } = new List<ThoughtCategoryAdditionalInfo>();
}
