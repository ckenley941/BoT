using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Core.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToFormattedDateString(this DateTime dt)
        {
            //Eventually need to handle different formats based on a configuration but for now at least this is all in one place
            return dt.ToString("MM/dd/yyyy");
        }

        public static string? ToFormattedDateString(this DateTime? dt)
        {
            if (dt == null) return null;

            return ToFormattedDateString(dt.Value);
        }
    }
}
