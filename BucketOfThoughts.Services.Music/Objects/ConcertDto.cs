using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using System.Text;

namespace BucketOfThoughts.Services.Music.Objects
{
    public class ConcertDto : BaseDto
    {
        public string BandName { get; set; }
        public string? Venue { get; set; }
        public DateOnly ConcertDate { get; set; }
        public string? ConcertDayOfWeek { get; set; }
        public string? Notes { get; set; }
        public List<SetlistSongDto> Songs { get; set; } = new List<SetlistSongDto>();
        public List<SetDto> Setlist
        {
            get
            {
                var setlist = new List<SetDto>();
                if (Songs.Any())
                {
                    var groupedSets = Songs.Select(s => new { s.SetNo, s.Name, s.HasCarrot, s.SongNo }).GroupBy(s => s.SetNo).ToList();
                    var sb = new StringBuilder();
                    foreach (var songs in groupedSets)
                    {
                        var set = songs.Take(songs.Count() - 1)
                            .Select(set => $"{set.Name}{(set.HasCarrot ? " > " : ", ")}")
                            .Union(songs.Skip(songs.Count() - 1).Take(1)
                            .Select(set => $"{set.Name}")
                            );
                        setlist.Add(new SetDto()
                        {
                            SetNo = songs.Key,
                            SetlistBody = string.Join("", set)
                        });
                    }
                }
                return setlist;
            }
        }

    }
}
