using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelpLac.Service.Interfaces
{
    public interface IServiceBase<TEntity, TId, TRepository>
         where TEntity : class
         where TRepository : IRepositoryBase<TEntity, TId>
    {
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(TId id);
        Task DeleteManyAsync(TEntity[] entityArray);
        Task<TEntity> GetByIdAsync(TId id);
        Task<bool> SaveChangeAsync();
    }
}