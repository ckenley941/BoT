namespace BucketOfThoughts.Services.Languages.Objects
{
    public class WordTranslationDto : WordDto
    {
        public WordTranslationDto() : base() { }
        public WordTranslationDto(int id, Guid? guid, string word) : base(id, guid, word)
        {
        }
        public WordDto PrimaryTranslation { get; set; }
        public List<string> Pronunication { get; set; }
    }
}
