using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Constants;
using BucketOfThoughts.Core.Infrastructure.Enums;
using BucketOfThoughts.Core.Infrastructure.Exceptions;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Thoughts
{
    public interface IThoughtsService 
    {
        Task<ThoughtDto> GetDtoByIdAsync(int id);
        Task<ThoughtDto> GetRandomThoughtAsync(int? thoughtBucketId);
        Task<IEnumerable<ThoughtGridDto>> GetGridAsync();
        Task<IEnumerable<ThoughtGridDto>> GetRelatedThoughtsGridAsync(int thoughtId);
        Task<Thought> InsertAsync(InsertThoughtDto newItem);
    }

    public class ThoughtsService : BaseCRUDService<Thought, ThoughtDto>, IThoughtsService
    {
        protected new readonly ThoughtsDbContext _dbContext;
        private int recentThoughtCount = 100; //TODO - make configurable

        public ThoughtsService(ThoughtsDbContext dbContext, IDistributedCache cache, IMapper mapper) : base(dbContext, cache, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<ThoughtDto> GetDtoByIdAsync(int id)
        {
            var thought = await base.GetByIdAsync(id, "ThoughtBucket,ThoughtDetails");
            return _mapper.Map<ThoughtDto>(thought);
        }

        public async Task<ThoughtDto> GetRandomThoughtAsync(int? thoughtBucketId)
        {
            //Eventually remove from cache what has already been used so we don't repeat random thoughts or added a flag
            var thoughts = await GetThoughtsFromCache();

            if (thoughtBucketId > 0)
            {
                thoughts = thoughts.Where(t => t.ThoughtBucketId == thoughtBucketId).ToList();
            }

            if (thoughts?.Count <= 0)
            {
                throw new NotFoundException("Random Thought");
            }

            var rand = new Random();

            var randThought = thoughts[rand.Next(thoughts.Count)];

            return _mapper.Map<ThoughtDto>(randThought);
        }

        public async Task<ThoughtDto> GetRecentThoughtAsync()
        {
            //Eventually remove from cache what has already been used so we don't repeat random thoughts or add a processing table to show recent ones that were prompted
            var thoughts = await GetThoughtsFromCache();

            thoughts = thoughts.OrderByDescending(t => t.CreatedDateTime).Take(recentThoughtCount).ToList();     
            var rand = new Random();
            var randThought = thoughts[rand.Next(thoughts.Count)];

            return _mapper.Map<ThoughtDto>(randThought);
        }

        public async Task<IEnumerable<ThoughtGridDto>> GetGridAsync()
        {
            var thoughts = (await GetThoughtsFromCache()).OrderByDescending(x => x.CreatedDateTime);
            return _mapper.Map<IEnumerable<ThoughtGridDto>>(thoughts);
        }

        public async Task<IEnumerable<ThoughtGridDto>> GetRelatedThoughtsGridAsync(int thoughtId)
        {
            var queryParams = new GetQueryParams<Thought>()
            {
                IncludeProperties = "ThoughtBucket,ThoughtDetails,RelatedThoughtThoughtId1Navigations,RelatedThoughtThoughtId2Navigations",
                Filter = (t) => (t.Id == thoughtId)
            };

            var relatedThoughts = GetRelatedThoughts(thoughtId);
            return _mapper.Map<IEnumerable<ThoughtGridDto>>(relatedThoughts);
        }

        public async Task<Thought> InsertAsync(InsertThoughtDto newItem)
        {
            var thought = new Thought()
            {
                Description = newItem.Description,
                ThoughtBucketId = newItem.ThoughtBucketId,
                TextType = newItem.TextType
            };

            if (newItem.TextType == TextTypes.PlainText.ToString())
            {
                if (newItem.Details.Any())
                {
                    //Reverse the order since the UI puts the latest detail at the top
                    newItem.Details.Reverse();
                    int sortOrder = 0;
                    foreach (var detail in newItem.Details)
                    {
                        sortOrder++;
                        thought.ThoughtDetails.Add(new()
                        {
                            Description = detail,
                            SortOrder = sortOrder
                        });
                    }
                }
            }

            else if (newItem.TextType == TextTypes.Json.ToString())
            {
                var i = 0;
                newItem.JsonDetails.Keys.ForEach((k) =>
                {
                    i++;
                    newItem.JsonDetails.Json = newItem.JsonDetails.Json.Replace($"Column{i}", k);
                });
                thought.ThoughtDetails.Add(new()
                {
                    Description = string.Join("|", newItem.JsonDetails.Keys),
                    SortOrder = 1
                });
                thought.ThoughtDetails.Add(new()
                {
                    Description = newItem.JsonDetails.Json,
                    SortOrder = 2
                });
            }

            if (newItem.WebsiteLinks.Any())
            {
                int sortOrder = 0;
                foreach (var link in newItem.WebsiteLinks)
                {
                    sortOrder++;
                    thought.ThoughtWebsiteLinks.Add(new ()
                        {
                            //WebsiteLink = new ()
                            //{
                            //    WebsiteUrl = link,
                            //    SortOrder = sortOrder
                            //}
                        });
                }
            }

            await base.InsertAsync(thought);
            return thought;
        }

        private IEnumerable<Thought>? GetRelatedThoughts(int thoughtId)
        {
            var relatedThought1 = _dbContext.RelatedThoughts.Where(x => x.ThoughtId1 == thoughtId)
                .Join(_dbContext.Thoughts.Include(x => x.ThoughtDetails).Include(x => x.ThoughtBucket), rt => rt.ThoughtId2, t => t.Id, (rt, t) => t);

            var relatedThought2 = _dbContext.RelatedThoughts.Where(x => x.ThoughtId2 == thoughtId)
                .Join(_dbContext.Thoughts.Include(x => x.ThoughtDetails).Include(x => x.ThoughtBucket), rt => rt.ThoughtId1, t => t.Id, (rt, t) => t);

            return relatedThought1.Union(relatedThought2).AsEnumerable();
        }

        private async Task<List<Thought>> GetThoughtsFromCache()
        {
            var queryParams = new GetQueryParams<Thought>()
            {
                IncludeProperties = "ThoughtBucket,ThoughtDetails"
            };
            return (await base.GetFromCacheAsync(CacheKeys.Thoughts, queryParams)).ToList();
        }

        public Task<Thought> InsertAsync(Thought newItem)
        {
            throw new NotImplementedException();
        }
    }
}