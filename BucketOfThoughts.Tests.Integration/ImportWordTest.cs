using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;
using BucketOfThoughts.Services.Music.Data;
using System.Text.Json;

namespace BucketOfThoughts.Tests.Integration
{
    public class ImportWordTest
    {
        protected readonly MusicDbContext dbContext;
        protected readonly WordsService wordsService;
        public ImportWordTest(MusicDbContext dbContext, WordsService wordsService) 
        { 
            this.dbContext = dbContext;
            this.wordsService = wordsService; 
        }

        [Theory(Skip = "Batch process and not a test.")]
        //[Theory]
        [InlineData(@"C:\Users\ckenl\Documents\Quick-Add-Word-Data.json")]
        public async Task ImportWord(string filePath)
        {
            InsertWordCardDto importData = new InsertWordCardDto();
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                importData = JsonSerializer.Deserialize<InsertWordCardDto>(json);
            }
        }

        [Theory(Skip = "Batch process and not a test.")]
        //[Theory]
        [InlineData(@"C:\Users\ckenl\Documents\Quick-Add-Word-Data.json")]
        public async Task ImportWords(string filePath)
        {
            List<InsertWordCardDto> importData = new List<InsertWordCardDto>();
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                importData = JsonSerializer.Deserialize<List<InsertWordCardDto>>(json);
            }
        }
    }
}