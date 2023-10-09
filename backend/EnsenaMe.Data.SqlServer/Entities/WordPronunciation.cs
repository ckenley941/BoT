using System;
using System.Collections.Generic;

namespace EnsenaMe.Data.Entities;

public partial class WordPronunciation
{
    public int WordPronunciationId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public int WordId { get; set; }

    public string Phonetic { get; set; } = null!;

    public virtual Word Word { get; set; } = null!;
}
