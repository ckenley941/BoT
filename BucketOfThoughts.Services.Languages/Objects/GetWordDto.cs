namespace BucketOfThoughts.Services.Languages.Objects
{
    public class GetWordDto
    {
        public int Word1Id { get; set; }
        public string Word1 { get; set; }
        public string Word1Example { get; set; }
        public int Word2Id { get; set; }
        public string Word2 { get; set; }
        public string Word2Example { get; set; }
        public DateTime RecordDateTime { get; set; }
    }
}
