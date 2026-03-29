using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        private protected OrderContext _orderContext;
        private readonly DbSet<T> _orderContextSet;

        public RepositoryBase(OrderContext orderContext)
        {
            _orderContext = orderContext;
            _orderContextSet = _orderContext.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _orderContextSet.AddAsync(entity);
            await _orderContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _orderContextSet.Remove(entity);
            await _orderContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _orderContextSet.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _orderContextSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _orderContextSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _orderContext.Entry(entity).State = EntityState.Modified;
            await _orderContext.SaveChangesAsync();
        }
    }
}
