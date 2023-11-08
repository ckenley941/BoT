using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Objects
{
    public class ThoughtCategoryDto : BaseDto
    {
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int? SortOrder { get; set; }
        public int ThoughtModuleId { get; set; }

    }
}
