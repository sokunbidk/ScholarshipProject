using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAsync(IList<int> ids);

        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> Query();

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task DeleteAsync(T entity);

        IQueryable<T> Query(Expression<Func<T, bool>> expression);

        Task<int> SaveChangesAsync();


        //for generic types
        Task<TEntity> GetAsync<TEntity>(int id) where TEntity : BaseEntity;
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task DeleteAsync<TEntity>(int id) where TEntity : BaseEntity, new();
        Task DeleteAsync<TEntity>(TEntity entity) where TEntity: BaseEntity;
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(IList<int> ids) where TEntity : BaseEntity;
        Task<bool> ExistsAsync<TEntity>(int id) where TEntity : BaseEntity;
        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : BaseEntity;
        IQueryable<TEntity> Query<TEntity>() where TEntity : BaseEntity;
        IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : BaseEntity;
    }
}
