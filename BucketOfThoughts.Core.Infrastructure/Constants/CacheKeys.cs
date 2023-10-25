using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Core.Infrastructure.Constants
{
    public static class CacheKeys
    {
        public const string Key = "cached";

        public const string DefaultModuleId = $"{Key}DefaultModuleId";
        public const string ThoughtCategories = $"{Key}ThoughtCategories";
    }
}
