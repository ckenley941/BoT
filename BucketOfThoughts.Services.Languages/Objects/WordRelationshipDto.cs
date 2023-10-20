namespace BucketOfThoughts.Services.Languages.Objects
{
    public class WordRelationshipDto : WordTranslationDto
    {
        public WordRelationshipDto() : base() { }
        public WordRelationshipDto(int id, Guid? guid, string word) : base(id, guid, word)
        {
        }
        public bool IsRelated { get; set; }
        public bool IsPhrase { get; set; }
        public bool IsSynonym { get; set; }
        public bool IsAntonym { get; set; }
    }
}
