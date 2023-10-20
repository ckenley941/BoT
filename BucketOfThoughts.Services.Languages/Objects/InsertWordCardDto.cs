using System.ComponentModel.DataAnnotations;

namespace BucketOfThoughts.Services.Languages.Objects
{
    public class InsertWordCardDto
    {
        [Required]
        public string PrimaryWord { get; set; }
        public List<InsertWordDto> WordDictionary { get; set; }
        public List<InsertWordPronunciation> Pronunication { get; set; }

    }
}
