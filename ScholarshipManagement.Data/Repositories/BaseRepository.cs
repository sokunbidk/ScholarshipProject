using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;

namespace ScholarshipManagement.Data.Repositories
{
    public abstract class BaseRepository<T>: IRepository<T> where T: BaseEntity, new()
    {
        protected SchoolDbContext DbContext { get; set; }

        public async Task<T> GetAsync(int id)
        {
            return await DbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAsync(IList<int> ids)
        {
            return await  DbContext.Set<T>()
                .Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async  Task<bool> ExistsAsync(int id)
        {
            return await DbContext.Set<T>()
                .AnyAsync(e => e.Id == id);
        }

        public IQueryable<T> Query()
        {
            return DbContext.Set<T>()
                .AsQueryable();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>()
                .Where(expression);
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbContext.Set<T>()
                .AddAsync(entity);
            return entity;
        }
        public Task<T> UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }
        public Task DeleteAsync(int id)
        {
            var entity = new T
            {
                Id = id
            };

            DbContext.Entry(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }
     
        public Task DeleteAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }


        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
        {
            await DbContext.AddRangeAsync(entities);
            return entities;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>()
               .AnyAsync(expression);
        }

        public Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public Task DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await DbContext.Set<TEntity>()
                .AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> GetAsync<TEntity>(int id) where TEntity : BaseEntity
        {
            return await DbContext.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task DeleteAsync<TEntity>(int id) where TEntity : BaseEntity, new()
        {
            var entity = new TEntity()
            {
                Id = id
            };
            DbContext.Entry(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(IList<int> ids) where TEntity : BaseEntity
        {
            return await DbContext.Set<TEntity>()
                .Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<bool> ExistsAsync<TEntity>(int id) where TEntity : BaseEntity
        {
            return await DbContext.Set<TEntity>()
                .AnyAsync(e => e.Id == id);
        }

        public async Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : BaseEntity
        {
            return await DbContext.Set<TEntity>()
                .AnyAsync(expression);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : BaseEntity
        {
            return DbContext.Set<TEntity>()
                .AsQueryable();
        }

        public IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : BaseEntity
        {
            return DbContext.Set<TEntity>()
                .Where(expression);
        }
    }
}
