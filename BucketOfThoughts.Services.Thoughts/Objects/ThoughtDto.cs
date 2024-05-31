using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Extensions;

namespace BucketOfThoughts.Services.Thoughts.Objects
{
    public class ThoughtDto : BaseDto
    {
        public string Description { get; set; } = string.Empty;
        public string TextType { get; set; } = string.Empty;
        public ThoughtCategoryDto Category { get; set; } = new ThoughtCategoryDto();
        public DateTimeOffset ThoughtDateTime { get; set; } = DateTimeOffset.UtcNow;
        public string ThoughtDateString => ThoughtDateTime.Date.ToFormattedDateString();
        public List<ThoughtDetailDto> Details { get; set; } = new List<ThoughtDetailDto>();
    }
}
