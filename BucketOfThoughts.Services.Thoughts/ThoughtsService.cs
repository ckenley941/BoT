using AutoMapper
    ;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

namespace BucketOfThoughts.Services.Thoughts
{
    public interface IThoughtsService : ICrudService<Thought, ThoughtDto>
    {
        Task<ThoughtDto> GetByIdAsync(int id);
        Task<ThoughtDto> GetRandomThoughtAsync();
        Task<IEnumerable<ThoughtGridDto>> GetGridAsync();
        Task<IEnumerable<ThoughtGridDto>> GetRelatedThoughtsGridAsync(int thoughtId);
        Task<Thought> InsertAsync(InsertThoughtDto newItem); //Eventually use ICRudService version of insert
    }

    public class ThoughtsService : BaseService<Thought, ThoughtDto>, IThoughtsService
    {
        protected new readonly IThoughtsRepository _repository;
        public ThoughtsService(IThoughtsRepository repository, IDistributedCache cache, IMapper mapper) : base (repository, cache, mapper)
        {
            _repository = repository;
        }

        public async Task<ThoughtDto> GetByIdAsync(int id)
        {
            var thought = await _repository.GetByIdAsync(id);
            return ConvertThoughtToDto(thought);
        }

        public async Task<ThoughtDto> GetRandomThoughtAsync()
        {
            //Eventually remove from cache what has already been used so we don't repeat random thoughts or added a flag
            var thoughts = await GetThoughtsFromCache();

            if (thoughts?.Count <= 0)
            {
                throw new Exception("Thoughts not found"); //TODO make custom not found exception passing in name of object not found "{} not found"
            }

            var rand = new Random();

            var randThought = thoughts[rand.Next(thoughts.Count)];

            return ConvertThoughtToDto(randThought);
        }

        public async Task<IEnumerable<ThoughtGridDto>> GetGridAsync()
        {
            var thoughts = (await GetThoughtsFromCache()).OrderByDescending(x => x.RecordDateTime);
            return ConvertThoughtToGridDto(thoughts);
        }

        public async Task<IEnumerable<ThoughtGridDto>> GetRelatedThoughtsGridAsync(int thoughtId)
        {
            var queryParams = new GetQueryParams<Thought>()
            {
                IncludeProperties = "ThoughtCategory,ThoughtDetails,RelatedThoughtThoughtId1Navigations,RelatedThoughtThoughtId2Navigations",
                Filter = (t) => (t.ThoughtId == thoughtId)
            };

            var relatedThoughts = _repository.GetRelatedThoughts(thoughtId);
            return ConvertThoughtToGridDto(relatedThoughts);
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

        private static ThoughtDto ConvertThoughtToDto(Thought thought)
        {
            return new ThoughtDto()
            {
                Id = thought.ThoughtId,
                Description = thought.Description,
                ThoughtDateTime = thought.RecordDateTime,
                Category = new ThoughtCategoryDto()
                {
                    Id = thought.ThoughtCategory.ThoughtCategoryId,
                    Description = thought.ThoughtCategory.Description
                },
                Details = thought.ThoughtDetails.Select(x => new ThoughtDetailDto()
                {
                    Id = x.ThoughtDetailId,
                    Description = x.Description
                }).ToList()
            };
        }

        private static IEnumerable<ThoughtGridDto> ConvertThoughtToGridDto(IEnumerable<Thought> thoughts)
        {
            return thoughts.Select(x => new ThoughtGridDto()
            {
                Id = x.ThoughtId,
                Description = x.Description,
                Category = x.ThoughtCategory.Description,
                Details = string.Join(", ", x.ThoughtDetails.Select(y => y.Description).ToList()),
            });
        }

    }
}