using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Languages.Data
{
    public class WordFlashCardSetDetail 
    {
        public int WordFlashCardSetId { get; set; }

        public int WordXrefId { get; set; }

        public virtual WordFlashCardSet WordFlashCardSet { get; set; } = null!;

        public virtual WordXref WordXref { get; set; } = null!;
    }
}
