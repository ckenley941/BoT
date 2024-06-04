using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Objects
{
    public class ThoughtBucketDto : BaseDto
    {
        public string Description { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int? SortOrder { get; set; }
        public int ThoughtModuleId { get; set; }

    }
}
