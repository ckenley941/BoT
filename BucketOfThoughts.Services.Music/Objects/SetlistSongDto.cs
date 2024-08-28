using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Music.Objects
{
    public class SetlistSongDto : BaseDto
    {
        public string SetNo { get; set; }
        public int SongNo { get; set; }
        public string Name { get; set; }
        public TimeSpan? SongLength { get; set; }
        public bool HasCarrot { get; set; } = false;
        public int? ShowGap { get; set; }
        public DateTime? ShowGapLastPlayedDate { get; set; }

    }
}
