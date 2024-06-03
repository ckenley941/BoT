using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.Extensions;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Core.Infrastructure.Objects;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Core.Infrastructure.BaseClasses
{
    public abstract class BaseService<TEntity, TDto> where TEntity : class where TDto : BaseDto
    {
        protected readonly ICrudRepository<TEntity> _repository;
        protected readonly IDistributedCache _cache;
        protected readonly IMapper _mapper;
        public BaseService(ICrudRepository<TEntity> repository, IDistributedCache cache, IMapper mapper)
        {
            _repository = repository;
            _cache = cache;
            _mapper = mapper;
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync(GetQueryParams<TEntity>? queryParams = null)
        {
            var data = await _repository.GetAsync(queryParams);
            return data;
        }

        public virtual async Task<IEnumerable<TEntity>> GetFromCacheAsync(string cacheKey, GetQueryParams<TEntity>? queryParams = null)
        {
            var data = await _cache.GetRecordAsync<IEnumerable<TEntity>>(cacheKey); 
            if (data == null)
            {
                data = await GetAsync(queryParams);
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

        public virtual async Task<TEntity> UpdateAsync(TDto updateItem)
        {
            var dbItem = await _repository.GetByIdAsync(updateItem.Id);
            if (dbItem == null)
            {
                throw new Exception("Not found."); //Make this custom not found exception
            }

            _mapper.Map(updateItem, dbItem);

            _repository.UpdateAsync(dbItem);
            await _repository.SaveAsync();
            return dbItem;
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        public virtual async Task DeleteAsync(TEntity deleteItem)
        {
            _repository.DeleteAsync(deleteItem);
            await _repository.SaveAsync();
        }
    }
}
