using System.ComponentModel.DataAnnotations;

namespace Common.Data.Objects.Words
{
    public class InsertWordCardDto
    {
        [Required]
        public string PrimaryWord { get; set; }
        public List<InsertWordDto> WordDictionary { get; set; }
        public List<InsertWordPronunciation> Pronunication { get; set; }

    }
}
