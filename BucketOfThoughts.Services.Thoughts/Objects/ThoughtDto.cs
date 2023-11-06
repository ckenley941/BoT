using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Thoughts.Objects
{
    public class ThoughtDto : BaseDto
    {
        public string Description { get; set; }
        public ThoughtCategoryDto Category { get; set; }
        public string ThoughtSource { get; set; }
        public List<ThoughtDetailDto> Details { get; set; }
    }
}
