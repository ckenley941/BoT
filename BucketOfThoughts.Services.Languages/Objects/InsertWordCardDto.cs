using System.ComponentModel.DataAnnotations;

namespace BucketOfThoughts.Services.Languages.Objects
{
    public class InsertWordCardDto
    {
        [Required]
        public string PrimaryWord { get; set; }
        public List<InsertWordDto> WordDictionary { get; set; } = new List<InsertWordDto>();
        public List<InsertWordPronunciation> Pronunication { get; set; } = new List<InsertWordPronunciation>();

    }
}
