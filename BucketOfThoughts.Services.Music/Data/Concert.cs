using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Music.Data;

public partial class Concert : BaseModifiableDbTable
{
    public int BandId { get; set; }
    public int? VenueId { get; set; }
    public DateOnly ConcertDate { get; set; }
    public string ConcertDayOfWeek { get; set; }
    public string Notes { get; set; }
    public string? Story { get; set; }
    public virtual Band Band { get; set; } = null!;
    public virtual Venue? Venue { get; set; }
    public virtual ICollection<SetlistSong> Songs { get; set; } = new List<SetlistSong>();
}
