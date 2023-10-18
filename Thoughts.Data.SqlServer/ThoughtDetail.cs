using System;
using System.Collections.Generic;

namespace Thoughts.Data.SqlServer;

public partial class ThoughtDetail
{
    public int ThoughtDetailId { get; set; }

    public Guid ThoughtDetailGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public string Description { get; set; } = null!;

    public int ThoughtId { get; set; }

    public int? SortOrder { get; set; }

    public virtual Thought Thought { get; set; } = null!;

    public virtual ICollection<ThoughtAdditionalInfo> ThoughtAdditionalInfos { get; } = new List<ThoughtAdditionalInfo>();
}
