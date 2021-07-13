using HelpLac.Domain.Entities.Base;
using HelpLac.Repository.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.Service.Interfaces
{
    public interface IServiceBase<TEntity, TId, TRepository>
         where TEntity : EntityBase
         where TRepository : IRepositoryBase<TEntity, TId>
    {
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteByIdAsync(TId id, CancellationToken cancellationToken);
        Task DeleteManyAsync(TEntity[] entityArray, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}