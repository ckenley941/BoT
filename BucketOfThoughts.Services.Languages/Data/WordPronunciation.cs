using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Languages.Data;

public partial class WordPronunciation : BaseDbTable
{
    public int WordId { get; set; }

    public string Phonetic { get; set; } = null!;

    public virtual Word Word { get; set; } = null!;
}
