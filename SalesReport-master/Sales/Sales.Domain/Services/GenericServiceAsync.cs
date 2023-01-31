using Sales.Domain.Entities;
using Sales.Domain.Interfaces.IRepository;
using Sales.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sales.Domain.Services
{
    public class GenericServiceAsync<T> : IGenericServiceAsync<T> where T : BaseEntity
    {
        private IGenericRepositoryAsync<T> repository;

        public GenericServiceAsync(IGenericRepositoryAsync<T> _repository)
        {
            repository = _repository;
        }

        public async Task Add(T entity)
        {
            await repository.Add(entity).ConfigureAwait(false);
        }
        public async Task AddRange(List<T> entity)
        {
            await repository.AddRange(entity).ConfigureAwait(false);
        }

        public async Task<int> CountAll()
        {
            return await repository.CountAll().ConfigureAwait(false);
        }

        public async Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return await repository.CountWhere(predicate);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await repository.FirstOrDefault(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await repository.GetAll();
        }

        public async ValueTask<T> GetById(Guid id)
        {
            return await repository.GetById(id).ConfigureAwait(false);
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate, int page, int size)
        {
            return repository.GetWhere(predicate, page, size);
        }

        public int CountFiltered(Expression<Func<T, bool>> predicate)
        {
            return repository.CountFiltered(predicate);
        }

        public async Task Remove(T entity)
        {
            await repository.Remove(entity).ConfigureAwait(false);
        }

        public async Task Update(T entity)
        {
            await repository.Update(entity).ConfigureAwait(false);
        }

        public async Task SaveAsync()
        {
            await repository.SaveAsync().ConfigureAwait(false);
        }
    }
}