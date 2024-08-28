using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Core.Infrastructure.Objects;

namespace BucketOfThoughts.Core.Infrastructure.Interfaces
{
    public interface ICrudService<TEntity, TDto> where TEntity : BaseDbTable where TDto : BaseDto
    {
        Task<TEntity> GetByIdAsync(int id, string? includeProperties = null);
        public Task<IEnumerable<TDto>> GetDtoByIdAsync(GetQueryParams<TEntity>? queryParams = null);
        public Task<IEnumerable<TEntity>> GetAsync(GetQueryParams<TEntity>? queryParams = null);
        public Task<IEnumerable<TEntity>> GetFromCacheAsync(string cacheKey, GetQueryParams<TEntity>? queryParams = null);
        public Task<IEnumerable<TDto>> GetDtoAsync(GetQueryParams<TEntity>? queryParams = null);
        public Task<TEntity> InsertAsync(TEntity itemToAdd, bool performSave = true, string cacheKey = "");
        public Task<TEntity> InsertDtoAsync(TDto newItem, bool performSave = true, string cacheKey = "");
        public Task<TEntity> UpdateAsync(TEntity updateItem, bool performSave = true, string cacheKey = "");
        public Task<TEntity> UpdateDtoAsync(TDto updateItem, bool performSave = true, string cacheKey = "");
        public Task DeleteAsync(int id, bool performSave = true);
        public Task DeleteAsync(TEntity deleteItem, bool performSave = true);
    }
}
