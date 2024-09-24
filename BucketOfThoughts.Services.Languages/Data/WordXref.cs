using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Languages.Data;

public partial class WordXref : BaseDbTable
{
    public int WordId1 { get; set; }

    public int WordId2 { get; set; }

    public bool? IsPrimaryTranslation { get; set; }

    public int SortOrder { get; set; }

    public virtual ICollection<WordContext> WordContexts { get; set; } = new List<WordContext>();
    public virtual ICollection<WordFlashCardSetDetail> WordFlashCardSetDetails { get; set; } = new List<WordFlashCardSetDetail>();

    public virtual Word WordId1Navigation { get; set; } = null!;

    public virtual Word WordId2Navigation { get; set; } = null!;
}
