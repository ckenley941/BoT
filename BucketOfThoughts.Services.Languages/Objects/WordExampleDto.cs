namespace BucketOfThoughts.Services.Languages.Objects
{ 
    public class WordExampleDto
    {
        public WordExampleDto() { }
        public WordExampleDto(int wordExampleId, string translation1, string translation2) 
        {
            WordExampleId = wordExampleId; ;
            Translation1 = translation1; 
            Translation2 = translation2;
        }

        public int WordExampleId { get; set; }
        public string Translation1 { get; set; }
        public string Translation2 { get; set; }
    }
}
