using HelpLac.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HelpLac.Repository
{
    public interface IRepositoryBase<TEntity, TId> where TEntity : EntityBase
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteByIdAsync(TId id);
        Task DeleteAsync(TEntity entity);
        Task DeleteManyAsync(TEntity[] entityArray);

        Task<TEntity> GetByIdAsync(TId id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> SearchAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);


        Task<bool> SaveChangesAsync();
    }
}
