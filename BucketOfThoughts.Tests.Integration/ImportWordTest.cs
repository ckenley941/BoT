using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;
using BucketOfThoughts.Services.Music.Data;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
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

        [Theory(Skip = "Batch process and not a test.")]
        //[Theory]
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

        //[Theory(Skip = "Batch process and not a test. This test is yet to be written.")]
        [Theory]
        [InlineData(@"C:\Users\ckenl\Documents\Add-Words-Data.csv", false)]
        public async Task ImportWordsFromCsv(string filePath, bool isDryRun)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.UTF8,
                Delimiter = ",",
                HasHeaderRecord = true, // skip first line  
            };

            List<InsertWordCsvDto> importData = new();

            StringBuilder errorBuilder = new();

            using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var textReader = new StreamReader(fs, Encoding.UTF8))
                using (var csv = new CsvReader(textReader, configuration))
                {

                    while (csv.Read())
                    {
                        try
                        {
                            var record = csv.GetRecord<InsertWordCsvDto>();
                            importData.Add(record);
                        }
                        catch (Exception ex)
                        {
                            errorBuilder.AppendLine(ex.Message);
                        }
                    }
                }

                if (errorBuilder.Length > 0)
                {
                    errorBuilder.Insert(0, $"Errors for {filePath}\n");

                    File.WriteAllText(
                        Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory, "ParseErrors.txt"),
                    errorBuilder.ToString());
                }
                else
                {
                }

                if (!isDryRun)
                {
                    if (importData?.Count > 0)
                    {
                        var insertData = new List<InsertWordCardDto>();
                        importData.ForEach(imp =>
                        {
                            var data = new InsertWordCardDto()
                            {
                                PrimaryWord = imp.PrimaryWord
                            };

                            var secondaryWords = imp.SecondaryWords.Split('|').ToList();
                            data.WordDictionary = secondaryWords.Select(sw =>
                                new InsertWordDto()
                                {
                                    Word = sw,
                                    IsPrimaryTranslation = secondaryWords.IndexOf(sw) == 0,
                                    SortOrder = secondaryWords.IndexOf(sw) + 1
                                }
                            ).ToList();

                            insertData.Add(data);
                        });

                        foreach (var row in insertData)
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
        public class InsertWordCsvDto
        {
            public string PrimaryWord { get; set; }
            public string SecondaryWords { get; set; }
        }
    }
}