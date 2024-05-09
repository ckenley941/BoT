namespace BucketOfThoughts.Core.Infrastructure.Interfaces
{
    public interface IDbTable
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
