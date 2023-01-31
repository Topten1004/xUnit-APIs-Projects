using Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sales.Domain.Interfaces.IRepository
{
    public interface IGenericRepositoryAsync<T> where T : BaseEntity
    {
        ValueTask<T> GetById(Guid id);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task Add(T entity);
        Task AddRange(List<T> entity);

        Task Update(T entity);

        Task Remove(T entity);

        Task<IEnumerable<T>> GetAll();

        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate, int page, int size);

        int CountFiltered(Expression<Func<T, bool>> predicate);

        Task<int> CountAll();

        Task<int> CountWhere(Expression<Func<T, bool>> predicate);

        Task SaveAsync();
    }
}