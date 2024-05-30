using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Extensions;

namespace BucketOfThoughts.Services.Thoughts.Objects
{
    public class ThoughtGridDto : BaseDto
    {
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string? DetailsLimited => Details?.ToStringLimit();
    }
}
