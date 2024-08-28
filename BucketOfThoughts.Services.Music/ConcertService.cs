using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Services.Music.Data;
using BucketOfThoughts.Services.Music.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Music
{

    public class ConcertService: BaseService<Concert, ConcertDto>
    {
        protected readonly new MusicDbContext _dbContext;
        public ConcertService(MusicDbContext dbContext, IDistributedCache cache, IMapper mapper) : base(dbContext, cache, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<ConcertDto> GetRandomConcertAsync()
        {
            var queryParams = new GetQueryParams<Concert>()
            {
                IncludeProperties = "Band,Venue,Songs",
            };
            var concerts = (await base.GetAsync(queryParams)).ToList();
            if (concerts?.Count <= 0)
            {
                throw new Exception("Concerts not found"); //TODO don't throw error?
            }

            var rand = new Random();

            var randConcert = concerts[rand.Next(concerts.Count)];

            return _mapper.Map<ConcertDto>(randConcert);
        }

    }   
}
