using BucketOfThoughts.Services.Music;
using BucketOfThoughts.Services.Music.Data;
using BucketOfThoughts.Services.Music.Objects;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Tests.Integration
{
    public class ImportConcertTest
    {
        protected readonly MusicDbContext dbContext;
        protected readonly ConcertService concertService;
        public ImportConcertTest(MusicDbContext dbContext, ConcertService concertService) 
        { 
            this.dbContext = dbContext;
            this.concertService = concertService; 
        }

        public static readonly object[][] ConcertData =
        {
            new object[] { "Phish", "Verizon Wireless Music Center", new DateOnly(2010, 8, 13), new List<SetDto>()
            {
                new ()
                {
                    SetNo = "1",
                    SetlistBody = "Chalk Dust Torture, Guelah Papyrus, My Sweet One, Axilla, I Didn't Know, Walls of the Cave, Stash, Train Song > Backwards Down the Number Line, Ocelot, The Ballad of Curtis Loew, Wilson > Possum"
                },
                new ()
                {
                    SetNo = "2",
                    SetlistBody = "Halley's Comet > Light > 46 Days > Maze, Meatstick -> The Mango Song > Fluffhead > Julius"
                },
                 new ()
                {
                    SetNo = "E",
                    SetlistBody = "Contact > Slave to the Traffic Light"
                }
            } }, 
        };


        [Theory, MemberData(nameof(ConcertData))]//(Skip = "Batch process and not a test.")]
        public async Task ImportConcert(string bandName, string venueName, DateOnly concertDate, List<SetDto> setlist)
        {
            var performSave = false;
            var band = await dbContext.Bands.SingleOrDefaultAsync(b => b.Name == bandName);
            if (band == null)
            {
                band = new Band() { Name = bandName };
                dbContext.Bands.Add(band);
                performSave = true;
            }
            var venue = await dbContext.Venues.SingleOrDefaultAsync(b => b.Name == venueName);
            if (venue == null)
            {
                venue = new Venue() { Name = venueName };
                dbContext.Venues.Add(venue);
                performSave = true;
            }
            if (performSave)
            {
                await dbContext.SaveChangesAsync();
            }
            else if (await dbContext.Concerts.AnyAsync(c => c.BandId == band.Id && c.VenueId == venue.Id && c.ConcertDate == concertDate))
            {
                Assert.Fail($"Concert for {bandName} at {venueName} on {concertDate} already exists in the database.");
            }

            var concert = await concertService.InsertBySetlistAsync(new InsertConcertDto()
            {
                BandId = band.Id,
                VenueId = venue.Id,
                ConcertDate = concertDate,
                ConcertDayOfWeek = concertDate.DayOfWeek.ToString(),
                Setlist = setlist
            });

            Assert.True(concert.Id > 0);
        }
    }
}