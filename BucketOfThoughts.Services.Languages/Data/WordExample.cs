using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Languages.Data;

public partial class WordExample : BaseModifiableDbTable
{
    public string Translation1 { get; set; } = null!;

    public string Translation2 { get; set; } = null!;

    public int WordContextId { get; set; }

    public virtual WordContext WordContext { get; set; } = null!;
}
