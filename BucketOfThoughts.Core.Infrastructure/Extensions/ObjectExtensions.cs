using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Core.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<object> AsEnumerable(this object obj)
        {
            yield return obj;
        }
    }
}
