using System;
using System.Collections.Generic;

namespace EnsenaMe.Data.Entities;

public partial class Language
{
    public int LanguageId { get; set; }

    public Guid LanguageGuid { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string Language1 { get; set; } = null!;

    public virtual ICollection<Word> Words { get; } = new List<Word>();
}
