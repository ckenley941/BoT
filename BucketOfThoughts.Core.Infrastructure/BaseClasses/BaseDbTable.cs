using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BucketOfThoughts.Core.Infrastructure.BaseClasses
{
    public abstract class BaseDbTable
    {
        [Column(Order = 1)]
        [Key]
        public int Id { get; set; }
        [Column(Order = 2)]
        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
