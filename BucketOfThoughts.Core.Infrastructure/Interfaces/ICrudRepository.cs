using BucketOfThoughts.Core.Infrastructure.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Core.Infrastructure.Interfaces
{
    public interface ICrudRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAsync();
        Task<IQueryable<TEntity>> GetAsync(GetQueryParams<TEntity> queryParams);
        Task<TEntity> GetByIdAsync(int id);
        Task InsertAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entiy);
        Task DeleteByIdAsync(int id);
        Task SaveAsync();
    }
}
