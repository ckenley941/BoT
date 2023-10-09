using System.ComponentModel.DataAnnotations;

namespace Common.Data.Objects.Words
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
