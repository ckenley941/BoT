using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Languages.Data;

public partial class WordContext : BaseModifiableDbTable
{
    public int WordXrefId { get; set; }

    public string ContextDesc { get; set; } = null!;

    public int SortOrder { get; set; }

    public virtual ICollection<WordExample> WordExamples { get; set; } = new List<WordExample>();

    public virtual WordXref WordXref { get; set; } = null!;
}
