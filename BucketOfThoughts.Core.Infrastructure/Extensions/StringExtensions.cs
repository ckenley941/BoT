using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Core.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string? ToStringLimit(this string str, int limit = 150)
        {
            if (str != null)
            {
                if (str.Length > limit)
                {
                    str = str[..(limit - 3)] + "...";
                }
            }
            return str;
        }
    }
}
