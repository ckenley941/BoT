using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using System.Text;

namespace BucketOfThoughts.Services.Music.Objects
{
    public class InsertConcertDto : BaseDto
    {
        public int BandId { get; set; }
        public int? VenueId { get; set; }
        public DateOnly ConcertDate { get; set; }
        public string? ConcertDayOfWeek { get; set; }
        public string? Notes { get; set; }
        public List<SetlistSongDto> Songs { get; set; } = new List<SetlistSongDto>();
        public List<SetDto> Setlist {  get; set; }
    }
}
