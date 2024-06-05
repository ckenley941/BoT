namespace BucketOfThoughts.Core.Infrastructure.Interfaces
{
    public interface IModifiableDbTable : IDbTable
    {
        public DateTimeOffset? ModifiedDateTime { get; set; }
    }
}
