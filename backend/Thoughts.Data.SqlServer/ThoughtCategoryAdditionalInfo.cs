using System;
using System.Collections.Generic;

namespace Thoughts.Data.SqlServer;

public partial class ThoughtCategoryAdditionalInfo
{
    public int ThoughtCategoryAdditionalInfoId { get; set; }

    public Guid ThoughtCategoryAdditionalInfoGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public int ThoughtCategoryId { get; set; }

    public string Description { get; set; } = null!;

    public int DataTypeId { get; set; }

    public int ColOrder { get; set; }

    public virtual DataType DataType { get; set; } = null!;

    public virtual ICollection<ThoughtAdditionalInfo> ThoughtAdditionalInfos { get; } = new List<ThoughtAdditionalInfo>();

    public virtual ThoughtCategory ThoughtCategory { get; set; } = null!;
}
