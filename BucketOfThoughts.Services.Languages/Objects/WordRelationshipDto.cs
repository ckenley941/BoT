namespace BucketOfThoughts.Services.Languages.Objects
{
    public class WordRelationshipDto : WordTranslationDto
    {
        public WordRelationshipDto() : base() { }
        public WordRelationshipDto(int id, string word) : base(id, word)
        {
        }
        public bool IsRelated { get; set; }
        public bool IsPhrase { get; set; }
        public bool IsSynonym { get; set; }
        public bool IsAntonym { get; set; }
    }
}
