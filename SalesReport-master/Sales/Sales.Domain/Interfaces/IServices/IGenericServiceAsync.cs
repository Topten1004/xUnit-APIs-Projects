using Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sales.Domain.Interfaces.IServices
{
    public interface IGenericServiceAsync<T> where T : BaseEntity
    {
        public ValueTask<T> GetById(Guid id);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        public Task Add(T entity);
        public Task AddRange(List<T> entity);

        public Task Update(T entity);

        public Task Remove(T entity);

        public Task<IEnumerable<T>> GetAll();

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate, int page, int size);

        public int CountFiltered(Expression<Func<T, bool>> predicate);

        public Task<int> CountAll();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate);

        Task SaveAsync();
    }
}