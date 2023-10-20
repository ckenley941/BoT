namespace BucketOfThoughts.Services.Languages.Data;

public partial class WordContext
{
    public int WordContextId { get; set; }

    public Guid WordContextGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public int WordXrefId { get; set; }

    public string ContextDesc { get; set; } = null!;

    public int SortOrder { get; set; }

    public virtual ICollection<WordExample> WordExamples { get; set; } = new List<WordExample>();

    public virtual WordXref WordXref { get; set; } = null!;
}
