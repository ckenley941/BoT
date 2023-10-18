using System;
using System.Collections.Generic;

namespace EnsenaMe.Data.Entities;

public partial class WordXref
{
    public int WordXrefId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public int WordId1 { get; set; }

    public int WordId2 { get; set; }

    public bool? IsPrimaryTranslation { get; set; }

    public int SortOrder { get; set; }

    public virtual ICollection<WordContext> WordContexts { get; } = new List<WordContext>();

    public virtual Word WordId1Navigation { get; set; } = null!;

    public virtual Word WordId2Navigation { get; set; } = null!;
}
