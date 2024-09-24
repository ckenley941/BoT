using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;
using BucketOfThoughts.Services.Music.Data;
using System.Text.Json;
using Xunit.Abstractions;

namespace BucketOfThoughts.Tests.Integration
{
    public class ImportWordTest
    {
        protected readonly ITestOutputHelper testOutput;
        protected readonly MusicDbContext dbContext;
        protected readonly WordsService wordsService;
        public ImportWordTest(ITestOutputHelper testOutput, MusicDbContext dbContext, WordsService wordsService) 
        { 
            this.testOutput = testOutput;
            this.dbContext = dbContext;
            this.wordsService = wordsService; 
        }

        [Theory(Skip = "Batch process and not a test.")]
        //[Theory]
        [InlineData(@"C:\Users\ckenl\Documents\Quick-Add-Word-Data.json")]
        public async Task ImportWord(string filePath)
        {
            using StreamReader r = new(filePath);
            string json = r.ReadToEnd();
            var importData = JsonSerializer.Deserialize<InsertWordCardDto>(json);
        }

        //[Theory(Skip = "Batch process and not a test.")]
        [Theory]
        [InlineData(@"C:\Users\ckenl\Documents\Add-Words-Data.json", false)]
        public async Task ImportWords(string filePath, bool isDryRun)
        {
            using StreamReader r = new(filePath);
            string json = r.ReadToEnd();
            var importData = JsonSerializer.Deserialize<List<InsertWordCardDto>>(json);

            if (!isDryRun)
            {
                if (importData?.Count > 0)
                {
                    foreach (var row in importData)
                    {
                        var wordId = await wordsService.InsertOrUpdateWordCardAsync(row);
                        if (wordId <= 0)
                        {
                            testOutput.WriteLine($"Inserting word {row.PrimaryWord} failed.");
                        }
                    }
                }
            }
        }

        //[Theory(Skip = "Batch process and not a test.")]
        [Theory]
        [InlineData(@"C:\Users\ckenl\Documents\Add-Words-Data.json", false)]
        public async Task ImportWordsFromCsv(string filePath, bool isDryRun)
        {
            //TODO - read from CSV: Word1, Word2
            //tocar, to touch|to play|to ring|to knock
            //OR Word2Def1, Word2Def2, Word2Def3 - and go out to certain number of definitions
            using StreamReader r = new(filePath);
            string json = r.ReadToEnd();
            var importData = JsonSerializer.Deserialize<List<InsertWordCardDto>>(json);

            if (!isDryRun)
            {
                if (importData?.Count > 0)
                {
                    foreach (var row in importData)
                    {
                        var wordId = await wordsService.InsertOrUpdateWordCardAsync(row);
                        if (wordId <= 0)
                        {
                            testOutput.WriteLine($"Inserting word {row.PrimaryWord} failed.");
                        }
                    }
                }
            }
        }
    }
}