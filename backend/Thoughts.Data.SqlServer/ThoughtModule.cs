using System;
using System.Collections.Generic;

namespace Thoughts.Data.SqlServer;

public partial class ThoughtModule
{
    public int ThoughtModuleId { get; set; }

    public Guid ThoughtModuleGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public string Description { get; set; } = null!;

    public int? SortOrder { get; set; }

    public virtual ICollection<ThoughtCategory> ThoughtCategories { get; } = new List<ThoughtCategory>();
}
