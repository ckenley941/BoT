using System;
using System.Collections.Generic;

namespace EnsenaMe.Data.Entities;

public partial class WordRelationship
{
    public int WordRelationshipId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public int WordId1 { get; set; }

    public int WordId2 { get; set; }

    public bool IsRelated { get; set; }

    public bool IsPhrase { get; set; }

    public bool IsSynonym { get; set; }

    public bool IsAntonym { get; set; }

    public virtual Word WordId1Navigation { get; set; } = null!;

    public virtual Word WordId2Navigation { get; set; } = null!;
}
