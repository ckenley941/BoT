using BucketOfThoughts.Core.Infrastructure.Extensions;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Core.Infrastructure.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.Core.Infrastructure.BaseClasses
{
    public abstract class BaseCrudRepositoryEF<TEntity> : ICrudRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected DbSet<TEntity> _dbSet;
        public BaseCrudRepositoryEF(DbContext context) 
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async virtual Task<IQueryable<TEntity>> GetAsync()
        {
            return await GetAsync(null);
        }

        public async virtual Task<IQueryable<TEntity>> GetAsync(GetQueryParams<TEntity>? queryParams)
        {
            queryParams ??= new GetQueryParams<TEntity> { };
            IQueryable<TEntity> query = _dbSet;

            if (queryParams.Filter != null)
            {
                query = query.Where(queryParams.Filter);
            }

            if (queryParams.IncludeProperties.HasValue())
            {
                foreach (var includeProperty in queryParams.IncludeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (queryParams.OrderBy != null)
            {
                return queryParams.OrderBy(query);
            }
            else
            {
                return query;
            }
        }

        public async virtual Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        public async virtual Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async virtual Task DeleteAsync(int id)
        {
            TEntity entity = await _dbSet.FindAsync(id);
            DeleteAsync(entity);
        }

        public virtual void DeleteAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }


        public async virtual Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
