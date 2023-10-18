using System;
using System.Collections.Generic;

namespace Thoughts.Data.SqlServer;

public partial class DataType
{
    public int DataTypeId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<ThoughtCategoryAdditionalInfo> ThoughtCategoryAdditionalInfos { get; } = new List<ThoughtCategoryAdditionalInfo>();
}
