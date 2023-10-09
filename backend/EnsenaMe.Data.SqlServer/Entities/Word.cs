using System;
using System.Collections.Generic;

namespace EnsenaMe.Data.Entities;

public partial class Word
{
    public int WordId { get; set; }

    public Guid WordGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string Description { get; set; } = null!;

    public int LanguageId { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual ICollection<WordPronunciation> WordPronunciations { get; } = new List<WordPronunciation>();

    public virtual ICollection<WordRelationship> WordRelationshipWordId1Navigations { get; } = new List<WordRelationship>();

    public virtual ICollection<WordRelationship> WordRelationshipWordId2Navigations { get; } = new List<WordRelationship>();

    public virtual ICollection<WordXref> WordXrefWordId1Navigations { get; } = new List<WordXref>();

    public virtual ICollection<WordXref> WordXrefWordId2Navigations { get; } = new List<WordXref>();
}
