using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Languages.Data;

public partial class WordFlashCardSet : BaseModifiableDbTable
{
    public string Description { get; set; } = null!;
    public virtual ICollection<WordFlashCardSetDetail> Details { get; set; } = new List<WordFlashCardSetDetail>();
}
