using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtsService : BaseService<Thought>
    {
        public ThoughtsService(ICrudRepository<Thought> repository, IDistributedCache cache) : base (repository, cache)
        {
        }

        public async Task<Thought> GetRandomThoughtAsync()
        {
            //Eventually remove from cache what has already been used so we don't repeat random thoughts or added a flag
            var thoughts = (await base.GetFromCacheAsync("Thoughts")).ToList();

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

            await base.InsertAsync(thought);
            return thought;
        }
    }
}