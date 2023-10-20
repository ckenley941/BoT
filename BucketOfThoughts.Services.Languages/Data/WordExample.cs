using System;
using System.Collections.Generic;

namespace BucketOfThoughts.Services.Languages.Data;

public partial class WordExample
{
    public int WordExampleId { get; set; }

    public Guid WordExampleGuid { get; set; }

    public DateTime RecordDateTime { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string Translation1 { get; set; } = null!;

    public string Translation2 { get; set; } = null!;

    public int WordContextId { get; set; }

    public virtual WordContext WordContext { get; set; } = null!;
}
