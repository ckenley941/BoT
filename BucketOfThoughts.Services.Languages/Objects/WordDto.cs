using System.ComponentModel.DataAnnotations;

namespace BucketOfThoughts.Services.Languages.Objects
{
    public class WordDto
    {
        public WordDto(){}

        public WordDto(int id, Guid? guid, string word)
        {
            Id = id;
            Guid = guid;
            Word = word;
            Examples = new List<WordExampleDto>();
        }

        public WordDto(int id, Guid? guid, string word, List<WordExampleDto> examples)
        {
            Id = id;
            Guid = guid;
            Word = word;
            Examples = examples;
        }

        [Key]
        public int Id { get; set; }
        public Guid? Guid { get; set; }
        public string Word { get; set; }
        public List<WordExampleDto> Examples { get; set; }
    }
}
