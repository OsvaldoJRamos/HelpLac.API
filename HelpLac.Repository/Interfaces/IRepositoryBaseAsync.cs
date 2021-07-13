using HelpLac.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity, TId> where TEntity : EntityBase
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteByIdAsync(TId id, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteManyAsync(TEntity[] entityArray, CancellationToken cancellationToken);

        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> SearchAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);


        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
