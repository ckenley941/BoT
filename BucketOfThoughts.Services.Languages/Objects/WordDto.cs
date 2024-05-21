using System.ComponentModel.DataAnnotations;

namespace BucketOfThoughts.Services.Languages.Objects
{
    public class WordDto
    {
        public WordDto(){}

        public WordDto(int id, string word)
        {
            Id = id;
            Word = word;
            Examples = new List<WordExampleDto>();
        }

        public WordDto(int id, string word, List<WordExampleDto> examples)
        {
            Id = id;
            Word = word;
            Examples = examples;
        }

        [Key]
        public int Id { get; set; }
        public string Word { get; set; }
        public List<WordExampleDto> Examples { get; set; }
    }
}
