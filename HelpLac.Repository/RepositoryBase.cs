using HelpLac.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelpLac.Repository
{
    public class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId> where TEntity : EntityBase
    {
        protected readonly Context _context;
        protected DbSet<TEntity> _dataset;

        public RepositoryBase(Context context)
        {
            _context = context;
            _dataset = _context.Set<TEntity>();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity) =>
            await Task.Run(() => _dataset.Update(entity).Entity);

        public async Task<TEntity> AddAsync(TEntity entity) =>
            await Task.Run(() => _dataset.Add(entity).Entity);

        public async Task DeleteAsync(TEntity entity) =>
            await Task.Run(() => _dataset.Remove(entity));

        public async Task DeleteByIdAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            await DeleteAsync(entity);
        }

        public async Task DeleteManyAsync(TEntity[] entityArray) =>
           await Task.Run(() => _dataset.RemoveRange(entityArray));

        public async Task<TEntity> GetByIdAsync(TId id) =>
            await _dataset.FindAsync(id);

        public async Task<List<TEntity>> GetAllAsync() =>
           await _dataset.ToListAsync();


        public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var result = _dataset.Where(i => true);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }

        /// <summary>
        /// Pesquisar por Predicates.
        /// http://appetere.com/post/passing-include-statements-into-a-repository
        /// </summary>
        /// <param name="predicate">O predicate.</param>
        /// <param name="includes">Os includes.</param>
        /// <returns></returns>
        public async Task<TEntity> SearchAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = _dataset.Where(predicate);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
