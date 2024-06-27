using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Music.Data
{
    public partial class SetlistSong : BaseModifiableDbTable
    {
        public int ConcertId { get; set; }
        public int SetNo { get; set; }
        public string SongNo { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan? SongLength { get; set; }   
        public bool HasCarrot { get; set; } = false;
        public int? ShowGap { get; set; }        
        public DateTime? ShowGapLastPlayedDate { get; set; }
        public virtual Concert Concert { get; set; } = null!;
    }
}
