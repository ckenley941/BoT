using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtsService : BaseService<Thought, ThoughtDto>
    {
        public ThoughtsService(ICrudRepository<Thought> repository, IDistributedCache cache, IMapper mapper) : base (repository, cache, mapper)
        {
        }

        public async Task<ThoughtDto> GetRandomThoughtAsync()
        {
            var queryParams = new GetQueryParams<Thought>()
            {
                IncludeProperties = "ThoughtCategory,ThoughtDetails"
            };

            //Eventually remove from cache what has already been used so we don't repeat random thoughts or added a flag
            var thoughts = await GetThoughtsFromCache();

            if (thoughts?.Count <= 0)
            {
                throw new Exception("Thoughts not found"); //TODO make custom not found exception passing in name of object not found "{} not found"
            }

            var rand = new Random();

            var randThought = thoughts[rand.Next(thoughts.Count)];

            return new ThoughtDto()
            {
                Id = randThought.ThoughtId,
                Description = randThought.Description,
                Category = new ThoughtCategoryDto()
                {
                    Id = randThought.ThoughtCategory.ThoughtCategoryId,
                    Description = randThought.ThoughtCategory.Description
                },
                Details = randThought.ThoughtDetails.Select(x => new ThoughtDetailDto()
                {
                    Id = x.ThoughtDetailId,
                    Description = x.Description
                }).ToList()
            };
        }

        public async Task<IEnumerable<ThoughtGridDto>> GetAsync()
        {
            var thoughts = (await GetThoughtsFromCache()).Select(x => new ThoughtGridDto()
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

        private async Task<List<Thought>> GetThoughtsFromCache()
        {
            var queryParams = new GetQueryParams<Thought>()
            {
                IncludeProperties = "ThoughtCategory,ThoughtDetails"
            };
            return (await base.GetFromCacheAsync("Thoughts", queryParams)).ToList();
        }

    }
}