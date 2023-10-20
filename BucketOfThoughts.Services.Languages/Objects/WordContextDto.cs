using System.ComponentModel.DataAnnotations;

namespace BucketOfThoughts.Services.Languages.Objects
{
    public class WordContextDto
    {
        public WordContextDto()
        {
            Words = new List<WordDto>();
        }
        public string ContextDesc { get; set; }
        public int SortOrder { get; set; }
        public List<WordDto> Words { get; set; }
    }
}
