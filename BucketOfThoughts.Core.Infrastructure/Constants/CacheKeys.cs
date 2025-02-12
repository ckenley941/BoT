﻿namespace BucketOfThoughts.Core.Infrastructure.Constants
{
    public static class CacheKeys
    {
        public const string Key = "cached"; //Eventually could cache per user or specific guid

        public const string DefaultModuleId = $"{Key}DefaultModuleId";
        public const string RecentlyViewedThoughts = $"{Key}RecentlyViewedThoughts";
        public const string ThoughtBuckets = $"{Key}ThoughtBuckets";
        public const string Thoughts = $"{Key}Thoughts";        
    }
}
