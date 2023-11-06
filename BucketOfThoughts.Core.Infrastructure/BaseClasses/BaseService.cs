using BucketOfThoughts.Core.Infrastructure.Extensions;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Core.Infrastructure.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Core.Infrastructure.BaseClasses
{
    public abstract class BaseService<TEntity> where TEntity : class
    {
        protected readonly ICrudRepository<TEntity> _repository;
        protected readonly IDistributedCache _cache;
        public BaseService(ICrudRepository<TEntity> repository, IDistributedCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public virtual async Task<IEnumerable<TEntity>> GetFromCacheAsync(string cacheKey)
        {
            var data = await GetFromCacheAsync(cacheKey, null);
            return data;
        }

        public virtual async Task<IEnumerable<TEntity>> GetFromCacheAsync(string cacheKey, GetQueryParams<TEntity>? queryParams)
        {
            var data = await _cache.GetRecordAsync<IEnumerable<TEntity>>(cacheKey); 
            if (data == null)
            {
                data = await _repository.GetAsync(queryParams);
                await _cache.SetRecordAsync(cacheKey, data);
            }

            return data;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity newItem)
        {
            await _repository.InsertAsync(newItem);
            await _repository.SaveAsync();
            return newItem;
        }

        public virtual async Task UpdateAsync(TEntity updateItem)
        {
            _repository.UpdateAsync(updateItem);
            await _repository.SaveAsync();
        }

        public virtual async Task DeleteAsync(TEntity deleteItem)
        {
            _repository.DeleteAsync(deleteItem);
            await _repository.SaveAsync();
        }
    }
}
