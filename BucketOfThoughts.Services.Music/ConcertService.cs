using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Exceptions;
using BucketOfThoughts.Core.Infrastructure.Objects;
using BucketOfThoughts.Services.Music.Data;
using BucketOfThoughts.Services.Music.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Services.Music
{

    public class ConcertService: BaseCRUDService<Concert, ConcertDto>
    {
        protected readonly new MusicDbContext _dbContext;
        public ConcertService(MusicDbContext dbContext, IDistributedCache cache, IMapper mapper) : base(dbContext, cache, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<Concert> InsertBySetlistAsync(InsertConcertDto concertDto)
        {
            var concert = new Concert()
            {
                BandId = concertDto.BandId,
                VenueId = concertDto.VenueId,
                ConcertDate = concertDto.ConcertDate,
                ConcertDayOfWeek = concertDto.ConcertDayOfWeek ?? concertDto.ConcertDate.DayOfWeek.ToString(),
                Notes = concertDto.Notes
            };

            concertDto.Setlist.ForEach(sl =>
            {
                var songNo = 1;
                var songs = sl.SetlistBody.Split(", ").ToList();                
                songs.ForEach(s =>
                {
                    var songsWithCarrots = s.Split(" > ").ToList();
                    //Creating copy of list so that if there is a jam sandwich we properly carrot the first and not the second
                    //i.e. YEM > Wilson > YEM  as we iterate through the songsWithCarrotsCopy list it will know to only carrot the first YEM and not the second one since it will be removed
                    var songsWithCarrotsCopy = songsWithCarrots.ConvertAll(s => s);
                    songsWithCarrots.ForEach(swc =>
                    {
                        var setlistSong = new SetlistSong()
                        {
                            Name = swc,
                            SetNo = sl.SetNo,
                            SongNo = songNo,
                        };

                        if (songsWithCarrotsCopy.IndexOf(swc) != songsWithCarrotsCopy.Count - 1)
                        {
                            setlistSong.HasCarrot = true;
                        }

                        songNo++;
                        concert.Songs.Add(setlistSong);
                        songsWithCarrotsCopy.Remove(swc);
                    });
                });
            }); 

            await base.InsertAsync(concert);
            return concert;
        }
    }   
}
