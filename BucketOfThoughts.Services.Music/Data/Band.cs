using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Music.Data
{
    public partial class Band : BaseModifiableDbTable
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Origin { get; set; }
        public int? FormationYear { get; set; }
        public virtual ICollection<Concert> Concerts { get; set; } = new List<Concert>();
    }
}
