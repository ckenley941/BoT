using Common.Data.Objects.Words;
using BucketOfThoughts.Services;
using EnsenaMe.Data.MongoDB;
using Common.Data.Objects.Thoughts;
using BucketOfThoughts.FileService;
using CsvHelper.Configuration;
using CsvHelper;
using System.IO;

namespace BucketOfThoughts.Api.Handlers
{
    public class ThoughtsHandler
    {
        private readonly IIngestionFileProcessor _ingestionFileProcessor;

        public ThoughtsHandler(IIngestionFileProcessor ingestionFileProcessor)
        {
            _ingestionFileProcessor = ingestionFileProcessor;
        }
        //public async Task<Thought> GetRandomThoughtAsync()
        //{
        //    //var thought = await _thoughtsService.GetRandomThoughtAsync();
        //    var a = Path.GetFullPath("../thoughts.xlsx");
        //    if (File.Exists("../thoughts.csv"))
        //    {
        //        try
        //        {
        //            var people = File.OpenRead("../thoughts.csv");
        //            await _ingestionFileProcessor.Process(people);
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    return null;
        //}
        //public async Task<Thought> AddThoughtAsync(InsertThoughtDto newThought)
        //{
        //    var thought = await _thoughtsService.AddThoughtAsync(newThought);
        //    return thought;
        //}

    }
}
