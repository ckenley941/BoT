using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Core.Infrastructure.Interfaces
{
    //TODO - standardize and use later...perhaps
    public interface ICrudService<TEntity, TDto> where TEntity : class where TDto : BaseDto
    {
        Task<IEnumerable<TEntity>> GetAsync(GetQueryParams<TEntity>? queryParams = null);
        Task<IEnumerable<TEntity>> GetFromCacheAsync(string cacheKey, GetQueryParams<TEntity>? queryParams = null);
        Task<TEntity> InsertAsync(TEntity newItem);
        Task<TEntity> UpdateAsync(TDto updateItem);
        Task DeleteAsync(int id);
        Task DeleteAsync(TEntity deleteItem);
    }
}
