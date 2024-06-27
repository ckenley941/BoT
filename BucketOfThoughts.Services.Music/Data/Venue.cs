using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Music.Data
{
    public partial class Venue : BaseModifiableDbTable
    {
        public string Name { get; set; } = null!;
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Description { get; set; }
        public bool IsFestival { get; set; } = false;
        public virtual ICollection<Concert> Concerts { get; set; } = new List<Concert>();
    }
}
