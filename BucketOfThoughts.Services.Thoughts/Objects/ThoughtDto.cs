using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Extensions;

namespace BucketOfThoughts.Services.Thoughts.Objects
{
    public class ThoughtDto : BaseDto
    {
        public string Description { get; set; }
        public ThoughtCategoryDto Category { get; set; }
        public DateTimeOffset ThoughtDateTime { get; set; }
        public string ThoughtDateString => ThoughtDateTime.Date.ToFormattedDateString();
        public List<ThoughtDetailDto> Details { get; set; }
    }
}
