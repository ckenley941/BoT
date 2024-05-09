namespace BucketOfThoughts.Core.Infrastructure.Interfaces
{
    internal interface IModifiableDbTable
    {
        public DateTimeOffset? ModifiedDateTime { get; set; }
    }
}
