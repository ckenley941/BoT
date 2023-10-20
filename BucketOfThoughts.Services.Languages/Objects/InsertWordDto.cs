using System.ComponentModel.DataAnnotations;

namespace BucketOfThoughts.Services.Languages.Objects
{
    public class InsertWordDto
    {
        [Required]
        public string Word { get; set; }
        public List<InsertWordContextDto> WordContexts { get; set; }
        public bool IsPrimaryTranslation { get; set; }
        public int SortOrder { get; set; }
        //public InsertWordRelationship WordRelationship { get; set; }
    }
}
