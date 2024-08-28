using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Services.Music.Data;
using BucketOfThoughts.Services.Music.Objects;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Music
{

    public class ConcertService: BaseService<Concert, ConcertDto>
    {
        public ConcertService(IConcertRepository repository, IDistributedCache cache, IMapper mapper) : base(repository, cache, mapper)
        {
        }

        public async Task<ConcertDto> GetRandomConcertAsync()
        {
            var queryParams = new GetQueryParams<Concert>()
            {
                IncludeProperties = "Band,Venue,Songs",
            };
            var concerts = (await _repository.GetAsync(queryParams)).ToList();

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
