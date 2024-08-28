using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.Exceptions;
using BucketOfThoughts.Core.Infrastructure.Extensions;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Core.Infrastructure.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Core.Infrastructure.BaseClasses
{
    public abstract class BaseCRUDService<TEntity, TDto> : ICrudService<TEntity, TDto> where TEntity : BaseDbTable where TDto : BaseDto 
    {
        protected readonly IDistributedCache _cache;
        protected readonly IMapper _mapper;
        protected readonly DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public BaseCRUDService(DbContext dbContext, IDistributedCache cache, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _cache = cache;
            _mapper = mapper;
        }

        public async virtual Task<TEntity> GetByIdAsync(int id, string? includeProperties = null)        
        {
            if (includeProperties.HasValue())
            {
                IQueryable<TEntity> query = _dbSet;
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                return await query.SingleOrDefaultAsync(q => q.Id == id) ?? throw new NotFoundException();
            }
            else
            {
                return await _dbSet.FindAsync(id) ?? throw new NotFoundException();
            }
        }

        public virtual async Task<IEnumerable<TDto>> GetDtoByIdAsync(GetQueryParams<TEntity>? queryParams = null)
        {
            var data = await GetAsync(queryParams);
            var dtoData = _mapper.Map<IEnumerable<TDto>>(data);
            return dtoData;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(GetQueryParams<TEntity>? queryParams = null)
        {
            queryParams ??= new GetQueryParams<TEntity> { };
            IQueryable<TEntity> query = _dbSet;

            if (queryParams.Filter != null)
            {
                query = query.Where(queryParams.Filter);
            }

            if (queryParams.IncludeProperties.HasValue())
            {
                foreach (var includeProperty in queryParams.IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (queryParams.OrderBy != null)
            {
                return await queryParams.OrderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
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

        public virtual async Task<IEnumerable<TDto>> GetDtoAsync(GetQueryParams<TEntity>? queryParams = null)
        {
            var data = await GetAsync(queryParams);
            var dtoData = _mapper.Map<IEnumerable<TDto>>(data);
            return dtoData;
        }
       
        public virtual async Task<TEntity> InsertAsync(TEntity itemToAdd, bool performSave = true, string cacheKey = "")
        {
            await _dbSet.AddAsync(itemToAdd);
            if (performSave)
            {
                await SaveAsync();
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    await _cache.RemoveAsync(cacheKey);
                }
            }
            return itemToAdd;
        }

        public virtual async Task<TEntity> InsertDtoAsync(TDto newItem, bool performSave = true, string cacheKey = "")
        {
            var itemToAdd = _mapper.Map<TEntity>(newItem);
            await _dbSet.AddAsync(itemToAdd);
            if (performSave)
            {
                await SaveAsync();
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    await _cache.RemoveAsync(cacheKey);
                }
            }
            return itemToAdd;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity updateItem, bool performSave = true, string cacheKey = "")
        {
            _dbSet.Attach(updateItem);
            _dbContext.Entry(updateItem).State = EntityState.Modified;
            if (performSave)
            {
                await SaveAsync();
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    await _cache.RemoveAsync(cacheKey);
                }
            }
            return updateItem;
        }

        public virtual async Task<TEntity> UpdateDtoAsync(TDto updateItem, bool performSave = true, string cacheKey = "")
        {
            var dbItem = await GetByIdAsync(updateItem.Id);
            _mapper.Map(updateItem, dbItem);

            _dbSet.Attach(dbItem);
            _dbContext.Entry(updateItem).State = EntityState.Modified;
            if (performSave)
            {
                await SaveAsync();
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    await _cache.RemoveAsync(cacheKey);
                }
            }
            return dbItem;
        }

        public virtual async Task DeleteAsync(int id, bool performSave = true)
        {
            TEntity entity = await GetByIdAsync(id);
            await DeleteAsync(entity);
            if (performSave)
            {
                await SaveAsync();
            }
        }

        public virtual async Task DeleteAsync(TEntity deleteItem, bool performSave = true)
        {
            if (_dbContext.Entry(deleteItem).State == EntityState.Detached)
            {
                _dbSet.Attach(deleteItem);
            }
            _dbSet.Remove(deleteItem);
            if (performSave)
            {
                await SaveAsync();
            }
        }

        public async virtual Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
