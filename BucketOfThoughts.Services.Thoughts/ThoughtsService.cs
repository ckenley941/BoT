using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtsService
    {
        private readonly ICrudRepository<Thought> _repository;
        private readonly ThoughtsDbContext _dbContext;
        private readonly IDistributedCache _cache;
        public ThoughtsService(ThoughtsDbContext dbContext, IDistributedCache cache, ICrudRepository<Thought> repository)
        {
            _dbContext = dbContext;
            _cache = cache;
            _repository = repository;
        }

        public async Task<Thought> GetRandomThoughtAsync()
        {
            //Eventually remove from cache what has already been used so we don't repeat random thoughts or added a flag
            //var thoughts = await _cache.GetRecordAsync<List<Thought>>("Thoughts");
            var thoughts = new List<Thought>();
            //if (thoughts == null)
            //{
            try
            {
                thoughts = (await _repository.GetAsync(includeProperties: "ThoughtDetails")).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            //    await _cache.SetRecordAsync("Thoughts", thoughts);
            //}


            if (thoughts?.Count <= 0)
            {
                throw new Exception("Thoughts not found"); //TODO make custom not found exception passing in name of object not found "{} not found"
            }

            var rand = new Random();
            return thoughts[rand.Next(thoughts.Count)];
        }

        public async Task<IEnumerable<GetThoughtDto>> GetAsync()
        {
            var thoughts = (await _repository.GetAsync()).Select(x => new GetThoughtDto()
            {
                Id = x.ThoughtId,
                Description = x.Description,
                Category = x.ThoughtCategory.Description,
                Details = string.Join(", ", x.ThoughtDetails.Select(y => y.Description).ToList()),
            });
            return thoughts;
        }

        public async Task<Thought> InsertAsync(InsertThoughtDto newItem)
        {
            var thought = new Thought()
            {
                Description = newItem.Description,
                ThoughtCategoryId = newItem.ThoughtCategoryId
            };

            if (newItem.Details?.Count > 0)
            {
                int sortOrder = 0;
                foreach (var detail in newItem.Details)
                {
                    sortOrder++;
                    thought.ThoughtDetails.Add(
                        new ThoughtDetail()
                        {
                            Description = detail,
                            SortOrder = sortOrder
                        });
                }
            }

            await _repository.InsertAsync(thought);
            await _repository.SaveAsync();

            return thought;
        }

        //public async Task<Thought> AddThoughtFromImportAsync(ThoughtVm newThought)
        //{
        //    //Check to see if thought already exists
        //    //var thought = await _dbContext.Thoughts.FirstOrDefaultAsync(x => x.Description == newThought.Thought);

        //    var thought = new Thought()
        //    {
        //        Description = newThought.Thought
        //    };

        //    if (newThought.Detail?.Count > 0)
        //    {
        //        int sortOrder = 0;
        //        foreach (var detail in newThought.Details)
        //        {
        //            sortOrder++;
        //            thought.ThoughtDetails.Add(
        //                new ThoughtDetail()
        //                {
        //                    Description = detail,
        //                    SortOrder = sortOrder
        //                });
        //        }
        //    }

        //    //_dbContext.Thoughts.Add(thought);
        //    //await _dbContext.SaveChangesAsync();

        //    //return thought;
        //}
    }
}