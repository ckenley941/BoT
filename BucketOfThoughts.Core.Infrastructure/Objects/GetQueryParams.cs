using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Core.Infrastructure.Objects
{
    public class GetQueryParams<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>>? Filter { get; set; }
        public string? IncludeProperties { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; set; }
    }
}
