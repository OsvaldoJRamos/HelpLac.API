using HelpLac.Domain.Entities.Base;
using HelpLac.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelpLac.Service
{
    public class ServiceBase<TEntity, TId, TRepository> : IServiceBase<TEntity, TId, TRepository>
                                     where TEntity : EntityBase
                                     where TRepository : IRepositoryBase<TEntity, TId>
    {

        protected readonly TRepository _repositorio;

        public ServiceBase(TRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var item = await _repositorio.UpdateAsync(entity);
            await _repositorio.SaveChangesAsync();
            return item;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Validate();
            var item = await _repositorio.AddAsync(entity);
            await _repositorio.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _repositorio.DeleteAsync(entity);
            await _repositorio.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(TId id)
        {
            await _repositorio.DeleteByIdAsync(id);
            await _repositorio.SaveChangesAsync();
        }

        public async Task DeleteManyAsync(TEntity[] entityArray)
        {
            await _repositorio.ExcluirVarios(entityArray);
            await _repositorio.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _repositorio.GetByIdAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _repositorio.SaveChangesAsync();
        }
    }
}