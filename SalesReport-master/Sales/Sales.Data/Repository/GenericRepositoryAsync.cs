using Microsoft.EntityFrameworkCore;
using Sales.Data.Context;
using Sales.Domain.Entities;
using Sales.Domain.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sales.Data.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : BaseEntity
    {
        protected SalesDBContext context;

        public GenericRepositoryAsync(SalesDBContext _context)
        {
            context = _context;
        }

        #region Public Methods

        public ValueTask<T> GetById(Guid id)
        {
            return context.Set<T>().FindAsync(id);
        }

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity).ConfigureAwait(false);
        }

        public async Task AddRange(List<T> entity)
        {
            await context.Set<T>().AddRangeAsync(entity).ConfigureAwait(false);
        }

        public async Task Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public async Task Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate, int page, int size)
        {
            return context.Set<T>().Where(predicate).Skip(page * size).Take(size).ToList();
        }

        public int CountFiltered(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).Count();
        }

        public async Task<int> CountAll()
        {
            return await context.Set<T>().CountAsync().ConfigureAwait(false);
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().CountAsync(predicate);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}