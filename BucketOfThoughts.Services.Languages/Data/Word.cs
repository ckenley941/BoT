using System;
using System.Collections.Generic;

namespace BucketOfThoughts.Services.Languages.Data;

public partial class Word
{
    public int WordId { get; set; }

    public Guid WordGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string Description { get; set; } = null!;

    public int LanguageId { get; set; }

    public virtual ICollection<WordPronunciation> WordPronunciations { get; set; } = new List<WordPronunciation>();

    public virtual ICollection<WordRelationship> WordRelationshipWordId1Navigations { get; set; } = new List<WordRelationship>();

    public virtual ICollection<WordRelationship> WordRelationshipWordId2Navigations { get; set; } = new List<WordRelationship>();

    public virtual ICollection<WordXref> WordXrefWordId1Navigations { get; set; } = new List<WordXref>();

    public virtual ICollection<WordXref> WordXrefWordId2Navigations { get; set; } = new List<WordXref>();
}
