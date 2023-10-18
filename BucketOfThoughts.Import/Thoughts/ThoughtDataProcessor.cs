using BucketOfThoughts.FileService;
using Common.Data.Objects.Thoughts;

namespace BucketOfThoughts.Imports.Thoughts
{
    public class ThoughtDataProcessor : IngestionFileProcessorBase, IIngestionFileProcessor
    {

        private readonly ICsvProcessor _csvProcessor;
        private FileIngestionResult _ingestionResult;
        public ThoughtDataProcessor(ICsvProcessor csvProcessor)
        {
            _csvProcessor = csvProcessor;
        }

        public async Task<FileIngestionResult> Process(FileStream stream)
        {
            _ingestionResult = new FileIngestionResult();

            //if (!await _csvProcessor.ValidateHeader<ThoughtVm, InsertThoughtClassMap>(stream))
            //{
            //    _ingestionResult.Errors.Add("The file type could not be identified. Please ensure that the provided file matches the expected header.");
            //    return _ingestionResult;
            //}

            var fileRecords = await GetRecordsAndLogErrors<ThoughtVm, ThoughtClassMap>(stream, _csvProcessor, _ingestionResult);

            if (_ingestionResult.Unprocessed != null && _ingestionResult.Unprocessed.Count > 0)
            {
                foreach (var unprocess in _ingestionResult.Unprocessed)
                {
                    _ingestionResult.Errors.Add(unprocess);
                }
                _ingestionResult.Unprocessed.Clear();
            }

            if (_ingestionResult.IsSuccessful)
            {
                var thoughts = fileRecords.GroupBy(x => x.Thought);
                foreach (var thought in thoughts)
                {
                    var newThought = new InsertThoughtDto()
                    {
                        Description = thought.Key,
                        ThoughtSource = thought.FirstOrDefault()?.Source,
                        Details = thought.Select(x => x.Detail).ToList()
                    };
                }
                //Handle File Ingestion - save records to DB - if category doesn't exist add it (do this depnending on force variable)
            }

            return _ingestionResult;
        }
    }
}