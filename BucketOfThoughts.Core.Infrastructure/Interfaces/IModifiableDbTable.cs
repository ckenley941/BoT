namespace BucketOfThoughts.Core.Infrastructure.Interfaces
{
    internal interface IModifiableDbTable : IDbTable
    {
        public DateTimeOffset? ModifiedDateTime { get; set; }
    }
}
