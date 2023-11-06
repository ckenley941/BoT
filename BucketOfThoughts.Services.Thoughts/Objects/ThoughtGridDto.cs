using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Services.Thoughts.Objects
{
    public class ThoughtGridDto : BaseDto
    {
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Details { get; set; }
        public string? DetailsLimited => Details?.ToStringLimit();
    }
}
