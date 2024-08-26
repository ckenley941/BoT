using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Shared.Data;

public partial class Note : BaseModifiableDbTable
{
    public string Description { get; set; } = null!;
}
