using BucketOfThoughts.Core.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace BucketOfThoughts.Core.Infrastructure.BaseClasses
{
    public abstract class BaseModifiableDbTable : BaseDbTable, IModifiableDbTable
    {
        [Column(Order = 3)]
        public DateTimeOffset? ModifiedDateTime { get; set; }
    }
}
