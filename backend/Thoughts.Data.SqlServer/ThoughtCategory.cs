﻿using System;
using System.Collections.Generic;

namespace Thoughts.Data.SqlServer;

public partial class ThoughtCategory
{
    public int ThoughtCategoryId { get; set; }

    public Guid ThoughtCategoryGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public string Description { get; set; } = null!;

    public int? ParentId { get; set; }

    public int? SortOrder { get; set; }

    public int? ThoughtModuleId { get; set; }

    public virtual ICollection<ThoughtCategoryAdditionalInfo> ThoughtCategoryAdditionalInfos { get; } = new List<ThoughtCategoryAdditionalInfo>();

    public virtual ThoughtModule? ThoughtModule { get; set; }

    public virtual ICollection<Thought> Thoughts { get; } = new List<Thought>();
}
